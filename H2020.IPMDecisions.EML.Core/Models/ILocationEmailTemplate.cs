namespace H2020.IPMDecisions.EML.Core.Models
{
    public interface ILocationEmailTemplate
    {
        TranslatedEmailBodyParts TranslatedEmailBodyParts { get; set; }
        TranslatedSharedEmailParts TranslatedSharedEmailParts { get; set; }
    }

    public class TranslatedEmailBodyParts
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Button { get; set; }
    }

    public class TranslatedSharedEmailParts
    {
        public string Greeting { get; set; }
        public string Disclaimer { get; set; }
        public string Footer { get; set; }
        public string Funding { get; set; }
        public string Thanks { get; set; }
        public string Team { get; set; }
    }
}