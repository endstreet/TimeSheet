using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using System.Text;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services
{
    public class UserSession : IUserSession
    {
        readonly UserService _db;
        readonly EmailService _sender;
        readonly NavigationManager _nav;
        public UserSession(UserService db, EmailService sender, NavigationManager nav)
        {
            _db = db;
            _sender = sender;
            _nav = nav;
            activeUser = new ActiveUser();
        }
        public ActiveUser activeUser { get; set; }

        public async Task Login(LoginModel li)
        {
            User? user = await _db.GetUserByEmailAsync(li.Email) ?? throw new Exception("Login failed!");
            if (user.OTP == li.OTP && user.OTP != null)
            {
                user.OTP = null;
                await _db.UpdateUserAsync(user.Id, user);
            }
            else
            {
                //Validate pass
                if (!VerifyPassword(li.Password, user.Password, user.Salt))
                {
                    throw new Exception("Login failed!");
                }
            }
            //Set active user
            activeUser = new ActiveUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Otp = user.OTP,
                Roles = await _db.GetUsersRolesAsync(user.Id)
            };
        }

        public async Task Activate(string otp)
        {
            User user = await _db.GetUserByOtpAsync(otp);
            //Find user
            if (user.Id < 1) //Already activated or invalid activation code
            {
                if (string.IsNullOrEmpty(activeUser.Email))
                {
                    throw new Exception("Invalid activation code!");
                }
                return;

            }
            if (user.OTP == otp)
            {
                user.OTP = null;
                await _db.UpdateUserAsync(user.Id, user);
            }
            else
            {
                throw new Exception("Activation failed!");
            }
            //Set active user
            activeUser = new ActiveUser
            {
                Name = user.Name,
                Email = user.Email,
                Otp = null,
                Roles = await _db.GetUsersRolesAsync(user.Id)
            };
        }

        public async Task Register(RegisterModel rg)
        {
            //Existing user?
            User? user = await _db.GetUserByEmailAsync(rg.Email);

            if (user != null)
            {

                throw new Exception("Existing user, please try login.");
            }
            //Matogen Account
            if (!rg.Email.ToLower().EndsWith("matogen.com"))
            {
                throw new Exception("Invalid email.");
            }
            //Match Passwords
            if (rg.Password != rg.RepeatPassword)
            {
                throw new Exception("Passwords mismatch.");
            }
            //Create user
            user = new User
            {
                Name = rg.FirstName,
                Email = rg.Email,
                Password = HashPasword(rg.Password, out var salt),//Get password hash
                Salt = salt
            };
            Random _rdm = new();
            user.OTP = _rdm.Next(10000, 99999).ToString();
            string uri = $"{_nav.BaseUri}account/activate/{user.OTP}";
            _sender.SendMail(user.Email, $"MaLogger Verification PIN: {user.OTP}", Environment.NewLine + $"<a href=\"{uri}\">Activate</a>");
            await _db.OnboardUser(user);

        }
        public void Logout()
        {
            activeUser = new ActiveUser();
        }

        public bool IsloggedIn()
        {
            return !string.IsNullOrEmpty(activeUser.Name);
        }

        public bool IsInRole(List<string> roles)
        {
            if (!IsloggedIn()) return false;
            return activeUser.IsInRole(roles);
        }

        //private async Task ResendOTP(User user)
        //{
        //    Random _rdm = new Random();
        //    user.OTP = _rdm.Next(10000, 99999).ToString();
        //    await _db.UpdateUserAsync(user.Id, user);
        //    _sender.SendMail(user.Email, $"MaLogger Verification PIN: {user.OTP}", "");
        //}

        #region password hashing
        const int keySize = 64;
        const int iterations = 350000;
        readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
        #endregion
    }
}
