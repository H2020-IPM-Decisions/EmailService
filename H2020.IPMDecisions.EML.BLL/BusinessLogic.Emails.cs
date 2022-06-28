using System;
using System.Linq;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.EmailTemplates;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Logging;

namespace H2020.IPMDecisions.EML.BLL
{
    public partial class BusinessLogic : IBusinessLogic
    {
        public async Task<GenericResponse> SendForgotPasswordEmail(ForgotPasswordEmailDto forgotPasswordEmail)
        {
            try
            {
                var toAddress = forgotPasswordEmail.ToAddress;
                AddTranslatedBodyPartsToEmail(forgotPasswordEmail, "forgot_password");
                AddTranslatedSharedPartsToEmail(forgotPasswordEmail);
                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.ForgotPasswordEmailTemplatePath,
                    forgotPasswordEmail);

                await emailSender.SendSingleEmailAsync(toAddress, forgotPasswordEmail.TranslatedEmailBodyParts.Subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendForgotPasswordEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendRegistrationEmail(RegistrationEmailDto registrationEmail)
        {
            try
            {
                var toAddress = registrationEmail.ToAddress;

                AddTranslatedBodyPartsToEmail(registrationEmail, "registration", registrationEmail.HoursToConfirmEmail);
                AddTranslatedSharedPartsToEmail(registrationEmail);

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.RegistrationEmailTemplatePath,
                    registrationEmail);

                await emailSender.SendSingleEmailAsync(toAddress, registrationEmail.TranslatedEmailBodyParts.Subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendRegistrationEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> ResendConfirmationEmail(RegistrationEmailDto registrationEmail)
        {
            try
            {
                var toAddress = registrationEmail.ToAddress;
                AddTranslatedBodyPartsToEmail(registrationEmail, "reconfirm_email");
                AddTranslatedSharedPartsToEmail(registrationEmail);

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.ReConfirmEmailEmailTemplatePath,
                    registrationEmail);

                await emailSender.SendSingleEmailAsync(toAddress, registrationEmail.TranslatedEmailBodyParts.Subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error ResendConfirmationEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendDataRequestEmail(DataShareDto dataRequestDto)
        {
            try
            {
                var toAddress = dataRequestDto.ToAddress;
                AddTranslatedBodyPartsToEmail(dataRequestDto, "data_request", dataRequestDto.DataRequesterName);
                AddTranslatedSharedPartsToEmail(dataRequestDto);

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.DataShareEmailTemplatePath,
                    dataRequestDto);

                await emailSender.SendSingleEmailAsync(toAddress, dataRequestDto.TranslatedEmailBodyParts.Subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendDataRequestEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendInactiveUserEmail(InactiveUserDto inactiveUserDto)
        {
            try
            {
                var toAddress = inactiveUserDto.ToAddress;
                AddTranslatedBodyPartsToEmail(inactiveUserDto, "inactive_user", inactiveUserDto.InactiveMonths, inactiveUserDto.AccountDeletionDate);
                AddTranslatedSharedPartsToEmail(inactiveUserDto);

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.InactiveUserEmailTemplatePath,
                    inactiveUserDto);

                await emailSender.SendSingleEmailAsync(toAddress, inactiveUserDto.TranslatedEmailBodyParts.Subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendInactiveUserEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendInternalReportEmail(InternalReportDto internalReportDto)
        {
            try
            {
                var toAddresses = internalReportDto.ToAddresses.Split(";").ToList();

                var dateTime = DateTime.Today.ToString("yyyy_MM_dd");
                var body = string.Format("Report for this week {0} attached. Thanks", dateTime);
                var subject = string.Format("IPM Decisions Report user week {0}", dateTime);

                await emailSender.SendEmailWithAttachmentAsync(toAddresses, subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error SendInternalReportEmail. {0}", ex.Message), ex);
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        private void AddTranslatedBodyPartsToEmail(EmailDto email, string emailType, params object[] arguments)
        {
            email.TranslatedEmailBodyParts = new TranslatedEmailBodyParts()
            {
                Button = this.jsonStringLocalizer[$"{emailType}.button"].ToString(),
                Subject = this.jsonStringLocalizer[$"{emailType}.subject"].ToString()
            };

            switch (emailType)
            {
                case "registration":
                    email.TranslatedEmailBodyParts.Body = this.jsonStringLocalizer[$"{emailType}.body", arguments].ToString();
                    break;
                case "data_request":
                    email.TranslatedEmailBodyParts.Body = this.jsonStringLocalizer[$"{emailType}.body", arguments].ToString();
                    break;
                case "inactive_user":
                    email.TranslatedEmailBodyParts.Body = this.jsonStringLocalizer[$"{emailType}.body", arguments].ToString();
                    break;
                default:
                    email.TranslatedEmailBodyParts.Body = this.jsonStringLocalizer[$"{emailType}.body"].ToString();
                    break;
            }
        }

        private void AddTranslatedSharedPartsToEmail(EmailDto email)
        {
            email.TranslatedSharedEmailParts = new TranslatedSharedEmailParts()
            {
                Greeting = this.jsonStringLocalizer[$"shared.greeting", email.ToAddress].ToString(),
                Disclaimer = this.jsonStringLocalizer["shared.disclaimer"].ToString(),
                Footer = this.jsonStringLocalizer["shared.footer"].ToString(),
                Funding = this.jsonStringLocalizer["shared.funding"].ToString(),
                Thanks = this.jsonStringLocalizer["shared.thanks"].ToString(),
                Team = this.jsonStringLocalizer["shared.team"].ToString()
            };
        }
    }
}