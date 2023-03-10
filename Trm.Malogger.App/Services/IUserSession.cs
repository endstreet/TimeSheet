using Trm.MaLogger.MsData.Views;

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
        Task Register(RegisterModel rg);
    }
}