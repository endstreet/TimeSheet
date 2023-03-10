using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Trm.MaLogger.App.Services
{
    public class EmailService : IDisposable, IEmailService
    {
        private bool disposed;
        private SmtpClient? client;
        private string _smtpServer;
        private int _port;
        private NetworkCredential _credential;
        private string _sendFrom;

        public EmailService(IOptions<EmailConfig> config)
        {
            _smtpServer = config.Value.Server;
            _port = int.Parse(config.Value.Port);
            _credential = new NetworkCredential(config.Value.User, config.Value.Pass);
            _sendFrom = config.Value.User;
        }
        public void SendMail(string to, string subject, string body)
        {
            client = new SmtpClient
            {
                Host = _smtpServer,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = _credential
            };

            MailMessage message = new MailMessage(_sendFrom, to);

            string mailbody = body;// $
            message.Subject = subject;// "File Request";
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            //Attachment attachment;
            //foreach (string file in attachments)
            //{
            //    attachment = new Attachment(file);
            //    message.Attachments.Add(attachment);
            //}

            try
            {
                client.Send(message);
            }
            catch
            {
                throw;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed objects
                if (client != null)
                {
                    client.Dispose();
                }
            }
            // Dispose unmanaged objects
            disposed = true;
        }
    }
}

