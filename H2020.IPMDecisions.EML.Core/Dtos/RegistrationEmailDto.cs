using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class RegistrationEmailDto : EmailDto
    {
        [Required]
        public string RegistrationToken { get; set; }
    }
}