using System.Collections.Generic;

namespace H2020.IPMDecisions.EML.Core.Models
{
    public class SendGridEmailingListObject
    {
        public SendGridEmailingListObject()
        {
            List_Ids = new List<string>();
            Contacts = new List<SendGridEmailingListContact>();
        }

        public List<string> List_Ids { get; set; }
        public List<SendGridEmailingListContact> Contacts { get; set; }
    }
}