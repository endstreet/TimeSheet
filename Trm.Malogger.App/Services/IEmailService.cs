namespace Trm.MaLogger.App.Services
{
    public interface IEmailService
    {
        void Dispose();
        void SendMail(string to, string subject, string body);
    }
}