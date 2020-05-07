using System.Threading.Tasks;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public interface IEmailSender
    {
        Task SendSingleEmailAsync(string toAddress, string subject, string body);
    }
}