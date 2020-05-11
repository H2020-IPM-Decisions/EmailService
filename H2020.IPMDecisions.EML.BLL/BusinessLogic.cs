using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IEmailSender emailSender;

        public BusinessLogic(IEmailSender emailSender)
        {
            this.emailSender = emailSender
                ?? throw new System.ArgumentNullException(nameof(emailSender));
        }

        public async Task<GenericResponse> SendRegistrationEmail(RegistrationEmailDto registrationEmail)
        {
            try
            {
                var toAddress = registrationEmail.ToAddress;
                var subject = registrationEmail.EmailSubject;
                var body = "Welcome to the website, this is your toke" + registrationEmail.RegistrationToken;

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