using System.Net.Mime;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2020.IPMDecisions.EML.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ControllerBase
    {
        public IEmailSender emailSender { get; }
        public AccountsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender 
                ?? throw new System.ArgumentNullException(nameof(emailSender));
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegistrationEmail", Name = "RegistrationEmail")]
        // POST: api/accounts/registrationemail
        public async Task<IActionResult> RegistrationEmail()
        {
            var toAddress = "ToAddress@test.com";
            var subject = "This is the subject";
            var body = "hello Im the body";

            await emailSender.SendSingleEmailAsync(toAddress, subject, body);

            return Ok();
        }
    }
}