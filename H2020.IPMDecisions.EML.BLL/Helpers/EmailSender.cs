using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Models;
using H2020.IPMDecisions.EML.BLL.Providers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using Microsoft.Extensions.Logging;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettingsProvider emailSettings;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(
            IOptions<EmailSettingsProvider> emailSettings,
            ILogger<EmailSender> logger)
        {
            this.emailSettings = emailSettings.Value
                ?? throw new System.ArgumentNullException(nameof(emailSettings));
            this.logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendSingleEmailAsync(string toAddress, string subject, string body, EmailPriority priority = EmailPriority.Normal)
        {
            try
            {
                var message = new MimeMessage
                {
                    Subject = subject,
                    Body = new BodyBuilder { HtmlBody = body }.ToMessageBody()
                };

                message.From.Add(new MailboxAddress(emailSettings.FromName, emailSettings.FromAddress));
                message.To.Add(InternetAddress.Parse(toAddress));

                SetEmailPriority(priority, message);

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.EnableSsl);

                    if (emailSettings.UseSmtpLoginCredentials)
                        await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                };
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendSingleEmailAsync. {0}", ex.Message), ex);
                throw ex;
            }
        }

        private static void SetEmailPriority(EmailPriority priority, MimeMessage message)
        {
            switch (priority)
            {
                case EmailPriority.High:
                    message.Priority = MessagePriority.Urgent;
                    message.Importance = MessageImportance.High;
                    message.XPriority = XMessagePriority.High;
                    break;

                case EmailPriority.Normal:
                    message.Priority = MessagePriority.Normal;
                    message.Importance = MessageImportance.Normal;
                    message.XPriority = XMessagePriority.Normal;
                    break;

                case EmailPriority.Low:
                    message.Priority = MessagePriority.NonUrgent;
                    message.Importance = MessageImportance.Low;
                    message.XPriority = XMessagePriority.Low;
                    break;
            }
        }
    }
}