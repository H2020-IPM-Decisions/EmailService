using System.Threading.Tasks;

namespace H2020.IPMDecisions.EML.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendSingleEmailAsync(string toAddress, string subject, string body);
    }
}