using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.BLL
{
    public interface IBusinessLogic
    {
        #region Transactional Emails
        Task<GenericResponse> SendDataRequestEmail(DataShareDto dataRequestDto);
        Task<GenericResponse> SendForgotPasswordEmail(ForgotPasswordEmailDto forgotPasswordEmail);
        Task<GenericResponse> SendRegistrationEmail(RegistrationEmailDto registrationEmail);
        Task<GenericResponse> ResendConfirmationEmail(RegistrationEmailDto registrationEmail);
        Task<GenericResponse> SendInactiveUserEmail(InactiveUserDto inactiveUserDto);
        Task<GenericResponse> AddEmailToQueue(InactiveUserDto inactiveUserDto);
        #endregion

        #region Emailing List
        Task<GenericResponse> DeleteContactFromMailingList(string contactEmail);
        Task<GenericResponse> GetContactFromMailingList(string contactEmail);
        Task<GenericResponse> UpsertContactToMailingList(EmailingListContactDto contactDto);
        Task<GenericResponse> SendInternalReportEmail(InternalReportDto internalReportDto);
        #endregion
    }
}
