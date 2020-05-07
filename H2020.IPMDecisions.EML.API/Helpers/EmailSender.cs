using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;

namespace H2020.IPMDecisions.EML.API.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;        

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value 
                ?? throw new System.ArgumentNullException(nameof(emailSettings));
        }

        public async Task SendSingleEmailAsync(string toAddress, string subject, string body)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(emailSettings.FromName, emailSettings.FromAddress));

                mimeMessage.To.Add(new MailboxAddress(toAddress));
                mimeMessage.Subject = subject;
                var bodyBuilder = new MimeKit.BodyBuilder
                {
                    HtmlBody = body
                };

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.EnableSsl);
                    client.Authenticate(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
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