using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.BLL.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace H2020.IPMDecisions.EML.BLL
{
    public partial class BusinessLogic : IBusinessLogic
    {
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;
        private readonly IMarketingEmailingList marketingEmailingList;
        private readonly IJsonStringLocalizer jsonStringLocalizer;
        private readonly ILogger<BusinessLogic> logger;

        private IEmailQueue emailQueue;

        public BusinessLogic(
            IEmailSender emailSender,
            IConfiguration configuration,
            IMarketingEmailingList marketingEmailingList,
            IJsonStringLocalizer jsonStringLocalizer,
            ILogger<BusinessLogic> logger,
            IEmailQueue emailQueue)
        {
            this.configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            this.emailSender = emailSender
                ?? throw new System.ArgumentNullException(nameof(emailSender));
            this.marketingEmailingList = marketingEmailingList
                ?? throw new ArgumentNullException(nameof(marketingEmailingList));
            this.jsonStringLocalizer = jsonStringLocalizer
                ?? throw new ArgumentNullException(nameof(jsonStringLocalizer));
            this.logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            this.emailQueue = emailQueue
                ?? throw new ArgumentNullException(nameof(emailQueue));

            this.emailQueue.BeginProcessing(SendInactiveUserEmail);
        }
    }
}