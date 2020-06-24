using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Providers;
using Microsoft.Extensions.Options;
using MimeKit;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettingsProvider emailSettings;        

        public EmailSender(IOptions<EmailSettingsProvider> emailSettings)
        {
            this.emailSettings = emailSettings.Value 
                ?? throw new System.ArgumentNullException(nameof(emailSettings));
        }

        public async Task SendSingleEmailAsync(string toAddress, string subject, string body)
        {
            try
            {
                var message = new MimeMessage
                {
                    Subject = subject,
                    Body = new BodyBuilder { HtmlBody = body }.ToMessageBody()
                };

                message.From.Add(InternetAddress.Parse(emailSettings.FromAddress));
                message.To.Add(InternetAddress.Parse(toAddress));

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.EnableSsl);

                    if (emailSettings.UseSmtpLoginCredentials)
                        await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                };
            }
            catch (System.Exception ex)
            {
                // ToDo Log error       
                throw ex;
            }            
        }
    }
}