using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.BLL
{
    public interface IBusinessLogic
    {
        Task<GenericResponse> SendRegistrationEmail(RegistrationEmailDto registrationEmail);
    }
}
