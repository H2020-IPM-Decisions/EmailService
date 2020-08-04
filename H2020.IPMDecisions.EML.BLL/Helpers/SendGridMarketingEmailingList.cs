using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using H2020.IPMDecisions.EML.BLL.Providers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public class SendGridMarketingEmailingList : IMarketingEmailingList
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly EmailSettingsProvider emailSettings;

        public SendGridMarketingEmailingList(
            IMapper mapper,
            IOptions<EmailSettingsProvider> emailSettings,
            IConfiguration configuration)
        {
            this.emailSettings = emailSettings.Value
                ?? throw new System.ArgumentNullException(nameof(emailSettings));
            this.mapper = mapper
                ?? throw new System.ArgumentNullException(nameof(mapper));
            this.configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<HttpStatusCode> DeleteContactAsync(string id)
        {
            try
            {
                var client = new SendGridClient(emailSettings.SmtpPassword);

                var response = await client.RequestAsync(
                    method: SendGridClient.Method.DELETE,
                    urlPath: string.Format("marketing/contacts?ids={0}", id));

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    var deserializeResponseError = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
                    // ToDo Return Error message or throw exception with error message
                }
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                // ToDo Log error       
                throw ex;
            }
        }

        public async Task<string> SearchContactAsync(string email)
        {
            try
            {                
                var ipmDecisionsListId = configuration["MailingListSettings:IPMDecisionsListId"].ToString();
                var queryString = string.Format("email LIKE '{0}' AND CONTAINS(list_ids, '{1}')", email.ToLower(), ipmDecisionsListId);
                var jsonObject = new JObject();
                jsonObject.Add("query", queryString);

                var client = new SendGridClient(emailSettings.SmtpPassword);
                var response = await client.RequestAsync(
                    method: SendGridClient.Method.POST,
                    urlPath: "marketing/contacts/search",
                    requestBody: jsonObject.ToString());

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var deserializeResponseError = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
                    // ToDo Return Error message or throw exception with error message
                }
                var deserializeResponseOK = JsonConvert.DeserializeObject<SendGridSearchResult>(response.Body.ReadAsStringAsync().Result);

                if (deserializeResponseOK.Contact_Count != 1)
                    return null;

                return deserializeResponseOK.Result.FirstOrDefault().Id;
            }
            catch (Exception ex)
            {
                // ToDo Log error       
                throw ex;
            }
        }

        public async Task<HttpStatusCode> UpsertContactAsync(EmailingListContactDto contactDto)
        {
            try
            {
                var sendGridContact = this.mapper.Map<SendGridEmailingListContact>(contactDto);

                var client = new SendGridClient(emailSettings.SmtpPassword);
                var sendGridObject = new SendGridEmailingListObject();
                sendGridObject.List_Ids.Add(configuration["MailingListSettings:IPMDecisionsListId"].ToString());
                sendGridObject.Contacts.Add(sendGridContact);

                var json = JsonConvert.SerializeObject(sendGridObject);
                var response = await client.RequestAsync(
                    method: SendGridClient.Method.PUT,
                    urlPath: "marketing/contacts",
                    requestBody: json.ToString());

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    var deserializeResponseError = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
                    // ToDo Return Error message or throw exception with error message
                }
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                // ToDo Log error       
                throw ex;
            }
        }
    }
}