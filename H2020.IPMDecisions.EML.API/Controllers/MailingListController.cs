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
    [Route("api/MailingList")]
    [Consumes("application/vnd.h2020ipmdecisions.email+json")]
    [TypeFilter(typeof(RequestHasTokenResourceFilter))]
    public class MailingListController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;
        public MailingListController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic
                ?? throw new ArgumentNullException(nameof(businessLogic));
        }    
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Contact", Name = "Contact")]
        // PUT: api/mailinglist/contact
        public async Task<IActionResult> Put([FromBody] EmailingListContactDto contactDto)
        {
            var response = await businessLogic.UpsertContactToMailingList(contactDto);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }
    }
}