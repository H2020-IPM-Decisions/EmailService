using System;
using System.Net;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Configuration;

namespace H2020.IPMDecisions.EML.BLL
{
    public partial class BusinessLogic : IBusinessLogic
    {
        public async Task<GenericResponse> AddNewContactToMailingList(EmailingListContactDto contactDto)
        {
            try
            {
                var responseCode = await marketingEmailingList.AddNewContactAsync(contactDto);

                if (responseCode != HttpStatusCode.OK)
                    return GenericResponseBuilder.NoSuccess("Something went wrong. Try again later");

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