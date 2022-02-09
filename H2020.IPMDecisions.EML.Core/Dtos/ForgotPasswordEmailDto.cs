using System.ComponentModel.DataAnnotations;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class ForgotPasswordEmailDto : EmailDto
    {
        [Required]
        public string CallbackUrl { get; set; }
    }
}