using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;

namespace H2020.IPMDecisions.EML.BLL
{
    public interface IBusinessLogic
    {
        Task SendRegistrationEmail(RegistrationEmailDto registrationEmail);
    }
}
