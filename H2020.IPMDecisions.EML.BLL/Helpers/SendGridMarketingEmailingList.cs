using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using H2020.IPMDecisions.EML.BLL.Providers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
                    var ds_response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
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