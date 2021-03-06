using System.Net;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public interface IMarketingEmailingList
    {
        Task<HttpStatusCode> DeleteContactAsync(string id);
        Task<string> SearchContactAsync(string email);
        Task<HttpStatusCode> UpsertContactAsync(EmailingListContactDto contactDto);
    }
}