using System.Net;
using System.Net.Mail;
using System.Text;

namespace Trm.MaLogger.App.Services
{
    public class IEmailService
    {
        private NetworkCredential _credential;
        private int _port;
        private string _smtpServer;
        private SmtpClient client;
        private bool disposed;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
            //client = new SmtpClient(_smtpServer, _port);
            //NetworkCredential basicCredential1 = _credential;
            //client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;

            MailMessage message = new MailMessage(to, to);

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
            catch (Exception ex)
            {
                throw;
            }
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