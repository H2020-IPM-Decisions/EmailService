using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using Microsoft.Extensions.Configuration;

namespace H2020.IPMDecisions.EML.BLL
{
    public partial class BusinessLogic : IBusinessLogic
    {
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;
        private readonly IMarketingEmailingList marketingEmailingList;

        public BusinessLogic(
            IEmailSender emailSender,
            IConfiguration configuration,
            IMarketingEmailingList marketingEmailingList)
        {
            this.configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            this.emailSender = emailSender
                ?? throw new System.ArgumentNullException(nameof(emailSender));
            this.marketingEmailingList = marketingEmailingList
                ?? throw new ArgumentNullException(nameof(marketingEmailingList));
        }
    }
}