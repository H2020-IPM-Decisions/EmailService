using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Interfaces;
using MimeKit;

namespace H2020.IPMDecisions.EML.API.Helpers
{
    public class EmailSender : IEmailSender
    {
        private string _smtpServer = "localhost";
        private int _smtpPort = 2525;
        private string _fromAddress = "test@test.com";
        private string _fromAddressTitle = "test";
        private string _username;
        private string _password;
        private bool _enableSsl = false;
        private bool _useDefaultCredentials;

        public EmailSender()
        {
        }

        public async Task SendSingleEmailAsync(string toAddress, string subject, string body)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_fromAddressTitle, _fromAddress));

                mimeMessage.To.Add(new MailboxAddress(toAddress));
                mimeMessage.Subject = subject;
                var bodyBuilder = new MimeKit.BodyBuilder
                {
                    HtmlBody = body
                };

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(_smtpServer, _smtpPort, _enableSsl);
                    // client.Authenticate(_username, _password);

                    await client.SendAsync(mimeMessage);

                    client.Disconnect(true);
                }
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
            
        }
    }
}