using System;
using System.Net.Mime;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.API.Filters;
using H2020.IPMDecisions.EML.BLL;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace H2020.IPMDecisions.EML.API.Controllers
{
    [ApiController]
    [Route("api/mailinglist")]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPut("contact", Name = "UpsertContact")]
        // PUT: api/mailinglist/contact
        public async Task<IActionResult> Put([FromBody] EmailingListContactDto contactDto)
        {
            var response = await businessLogic.UpsertContactToMailingList(contactDto);

            if (response.IsSuccessful)
                return Ok();

            return BadRequest(new { message = response.ErrorMessage });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet("contact/{email}", Name = "GetContact")]
        // GET: api/mailinglist/contact/{email}
        public async Task<IActionResult> Get([FromRoute] string email)
        {
            var response = await businessLogic.GetContactFromMailingList(email);

            if (!response.IsSuccessful)
                return BadRequest(new { message = response.ErrorMessage });

            var responseAsJson = (GenericResponse<JObject>)response;
            if (responseAsJson.Result == null)
                return NotFound();

            return Ok(responseAsJson.Result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("contact/{email}", Name = "DeleteContact")]
        // DELETE: api/mailinglist/contact/{email}
        public async Task<IActionResult> Delete([FromRoute] string email)
        {
            var response = await businessLogic.DeleteContactFromMailingList(email);

            if (response.IsSuccessful)
                return NoContent();

            return BadRequest(new { message = response.ErrorMessage });
        }
    }
}