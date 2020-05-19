using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Configuration;

namespace H2020.IPMDecisions.EML.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public BusinessLogic(
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            this.emailSender = emailSender
                ?? throw new System.ArgumentNullException(nameof(emailSender));
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