using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
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
                    "EmailTemplates.ForgotPasswordEmailTemplate",
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
                    "EmailTemplates.RegistrationEmailTemplate",
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
    }
}