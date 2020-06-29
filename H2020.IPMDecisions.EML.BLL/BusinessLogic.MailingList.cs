using System;
using System.Net;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.BLL
{
    public partial class BusinessLogic : IBusinessLogic
    {
        public async Task<GenericResponse> UpsertContactToMailingList(EmailingListContactDto contactDto)
        {
            try
            {
                var responseCode = await marketingEmailingList.UpsertContactAsync(contactDto);

                if (responseCode != HttpStatusCode.Accepted)
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