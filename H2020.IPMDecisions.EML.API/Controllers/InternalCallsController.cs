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
    [Route("api/internalcall")]
    [Consumes("application/vnd.h2020ipmdecisions.internal+json")]
    [TypeFilter(typeof(RequestHasTokenResourceFilter))]
    public class InternalCallsController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;
        public InternalCallsController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic
                ?? throw new ArgumentNullException(nameof(businessLogic));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegistrationEmail", Name = "RegistrationEmail")]
        // POST: api/internalcall/registrationemail
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
        // POST: api/internalcall/forgotpassword
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
        // POST: api/internalcall/reconfirmemail
        public async Task<IActionResult> ReConfirmEmail([FromBody] RegistrationEmailDto registrationEmail)
        {
            var response = await businessLogic.ResendConfirmationEmail(registrationEmail);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SendDataRequest", Name = "SendDataRequest")]
        // POST: api/internalcall/SendDataRequest
        public async Task<IActionResult> SendDataRequest([FromBody] DataShareDto dataRequestDto)
        {
            var response = await businessLogic.SendDataRequestEmail(dataRequestDto);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SendInactiveUser", Name = "SendInactiveUser")]
        // POST: api/internalcall/SendInactiveUser
        public async Task<IActionResult> SendInactiveUser([FromBody] InactiveUserDto inactiveUserDto)
        {
            var response = await businessLogic.SendInactiveUserEmail(inactiveUserDto);

            if (response.IsSuccessful)
                return Ok();

            return
             BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddInactiveUserToQueue", Name = "AddInactiveUserToQueue")]
        // POST: api/internalcall/AddInactiveUserToQueue
        public IActionResult AddInactiveUserToQueue([FromBody] InactiveUserDto inactiveUserDto)
        {
            var response = businessLogic.AddEmailToQueue(inactiveUserDto);

            if(response.IsSuccessful)
                return Ok();

            return
             BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SendInternalReport", Name = "SendInternalReport")]
        // POST: api/internalcall/SendInternalReport
        public async Task<IActionResult> SendInternalReport([FromBody] InternalReportDto inactiveUserDto)
        {
            var response = await businessLogic.SendInternalReportEmail(inactiveUserDto);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }
    }
}