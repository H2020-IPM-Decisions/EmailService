using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class InactiveUserDto : EmailDto
    {
        [Required]
        public int InactiveMonths { get; set; }
        [Required]
        public string AccountDeletionDate { get; set; }
    }
}