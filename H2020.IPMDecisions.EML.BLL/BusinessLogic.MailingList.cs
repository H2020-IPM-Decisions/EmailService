using System;
using System.Net;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Newtonsoft.Json.Linq;

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

        public async Task<GenericResponse> DeleteContactFromMailingList(string contactEmail)
        {
            try
            {
                var contactId = await marketingEmailingList.SearchContactAsync(contactEmail);

                if (contactId != null)
                {
                    var responseCode = await marketingEmailingList.DeleteContactAsync(contactId);
                    if (responseCode != HttpStatusCode.Accepted)
                        return GenericResponseBuilder.NoSuccess("Something went wrong. Try again later");
                }                 

                return GenericResponseBuilder.Success();
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }

        public async Task<GenericResponse> GetContactFromMailingList(string contactEmail)
        {
            try
            {
                var contactId = await marketingEmailingList.SearchContactAsync(contactEmail);

                if (contactId == null) return GenericResponseBuilder.Success<JObject>(null);

                var jsonObject = new JObject();
                jsonObject.Add("id", contactId);
                return GenericResponseBuilder.Success<JObject>(jsonObject);
            }
            catch (Exception ex)
            {
                // ToDo log Error         
                return GenericResponseBuilder.NoSuccess(ex.Message.ToString());
            }
        }
    }
}