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
                var subject = configuration["EmailTemplates:ForgotPassword:Subject"];

                var body = await TemplateHelper.GetEmbeddedTemplateHtmlAsStringAsync(
                    EmailTemplates.ForgotPasswordEmailTemplatePath,
                    forgotPasswordEmail);

                await emailSender.SendSingleEmailAsync(toAddress, subject, body);
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
                var subject = configuration["EmailTemplates:Registration:Subject"];

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
    }
}