namespace Trm.MaLogger.App.Services
{
    public class EmailConfig : IEmailConfig
    {
        public string Server { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Pass { get; set; } = null!;

    }
}
