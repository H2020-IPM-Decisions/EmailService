using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public interface IEmailSender
    {
        Task SendSingleEmailAsync(string toAddress, string subject, string body, EmailPriority priority = EmailPriority.Normal);
    }
}