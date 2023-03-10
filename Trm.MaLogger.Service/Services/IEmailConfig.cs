namespace Trm.MaLogger.App.Services
{
    public interface IEmailConfig
    {
        string Pass { get; set; }
        string Port { get; set; }
        string Server { get; set; }
        string User { get; set; }
    }
}