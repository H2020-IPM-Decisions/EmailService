using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class ForgotPasswordEmailDto : EmailDto
    {
        [Required]
        public string CallbackUrl { get; set; }
    }
}