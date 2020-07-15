namespace H2020.IPMDecisions.EML.BLL.Providers
{
    public class EmailSettingsProvider
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSmtpLoginCredentials { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public bool EnableSsl { get; set; }
    }
}