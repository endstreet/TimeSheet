using Trm.MaLogger.MsData.Views;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services
{
    public interface IUserSession
    {
        ActiveUser activeUser { get; set; }

        Task Activate(string otp);
        bool IsInRole(List<string> roles);
        bool IsloggedIn();
        Task Login(LoginModel li);
        void Logout();
        Task<User> Register(RegisterModel rg);
        void SendOTP(User user,string baseURL);
    }
}