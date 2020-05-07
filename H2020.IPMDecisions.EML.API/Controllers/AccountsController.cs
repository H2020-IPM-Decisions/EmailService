using System;
using System.Net.Mime;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL;
using H2020.IPMDecisions.EML.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2020.IPMDecisions.EML.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;
        public AccountsController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic
                ?? throw new ArgumentNullException(nameof(businessLogic));
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegistrationEmail", Name = "RegistrationEmail")]
        // POST: api/accounts/registrationemail
        public async Task<IActionResult> RegistrationEmail([FromBody] RegistrationEmailDto registrationEmail)
        {
            await businessLogic.SendRegistrationEmail(registrationEmail);           

            return Ok();
        }
    }
}