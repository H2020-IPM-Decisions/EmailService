using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.API.Filters;
using H2020.IPMDecisions.EML.BLL;
using H2020.IPMDecisions.EML.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2020.IPMDecisions.EML.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Consumes("application/vnd.h2020ipmdecisions.email+json")]
    [TypeFilter(typeof(RequestHasTokenResourceFilter))]
    public class AccountsController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;
        public AccountsController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic
                ?? throw new ArgumentNullException(nameof(businessLogic));
        }    
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegistrationEmail", Name = "RegistrationEmail")]
        // POST: api/accounts/registrationemail
        public async Task<IActionResult> RegistrationEmail([FromBody] RegistrationEmailDto registrationEmail)
        {
            var response = await businessLogic.SendRegistrationEmail(registrationEmail);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        // POST: api/accounts/forgotpassword
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordEmailDto forgotPasswordEmail)
        {
            var response = await businessLogic.SendForgotPasswordEmail(forgotPasswordEmail);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ReConfirmEmail", Name = "ReConfirmEmail")]
        // POST: api/accounts/reconfirmemail
        public async Task<IActionResult> ReConfirmEmail([FromBody] RegistrationEmailDto registrationEmail)
        {
            var response = await businessLogic.ResendConfirmationEmail(registrationEmail);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }
    }
}