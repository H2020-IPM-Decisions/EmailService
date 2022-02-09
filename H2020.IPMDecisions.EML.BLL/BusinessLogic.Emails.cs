using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.EmailTemplates;
using H2020.IPMDecisions.EML.Core.Models;

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
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendRegistrationEmail(RegistrationEmailDto registrationEmail)
        {
            try
            {
                var toAddress = registrationEmail.ToAddress;
                var subject = this.jsonStringLocalizer["registration.subject"].ToString();

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.RegistrationEmailTemplatePath,
                    registrationEmail);

                await emailSender.SendSingleEmailAsync(toAddress, subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> ResendConfirmationEmail(RegistrationEmailDto registrationEmail)
        {
            try
            {
                var toAddress = registrationEmail.ToAddress;
                var subject = configuration["EmailTemplates:ReConfirmEmail:Subject"];

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.ReConfirmEmailEmailTemplatePath,
                    registrationEmail);

                await emailSender.SendSingleEmailAsync(toAddress, subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendDataRequestEmail(DataShareDto dataRequestDto)
        {
            try
            {
                var toAddress = dataRequestDto.ToAddress;
                var subject = configuration["EmailTemplates:DataRequest:Subject"];

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.DataShareEmailTemplatePath,
                    dataRequestDto);

                await emailSender.SendSingleEmailAsync(toAddress, subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> SendInactiveUserEmail(InactiveUserDto inactiveUserDto)
        {
            try
            {
                var toAddress = inactiveUserDto.ToAddress;
                var subject = configuration["EmailTemplates:InactiveUser:Subject"];

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.InactiveUserEmailTemplatePath,
                    inactiveUserDto); ;

                await emailSender.SendSingleEmailAsync(toAddress, subject, body);
                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }


        private void AddTranslatedBodyPartsToEmail(EmailDto email, string emailType)
        {
            email.TranslatedEmailBodyParts = new TranslatedEmailBodyParts()
            {
                Body = this.jsonStringLocalizer[$"{emailType}.body"].ToString(),
                Button = this.jsonStringLocalizer[$"{emailType}.button"].ToString(),
                Subject = this.jsonStringLocalizer[$"{emailType}.subject"].ToString()
            };
        }

        private void AddTranslatedSharedPartsToEmail(EmailDto email)
        {
            email.TranslatedSharedEmailParts = new TranslatedSharedEmailParts()
            {
                Greeting = this.jsonStringLocalizer[$"shared.greeting"].ToString(),
                Disclaimer = this.jsonStringLocalizer["shared.disclaimer"].ToString(),
                Footer = this.jsonStringLocalizer["shared.footer"].ToString(),
                Funding = this.jsonStringLocalizer["shared.funding"].ToString(),
                Thanks = this.jsonStringLocalizer["shared.thanks"].ToString(),
                Team = this.jsonStringLocalizer["shared.team"].ToString()
            };
        }
    }
}