using System.Collections.Generic;

namespace H2020.IPMDecisions.EML.Core.Models
{
    public class SendGridSearchResult
    {
        public List<SendGridEmailingListContact> Result { get; set; }
        public int Contact_Count { get; set; }
    }
}