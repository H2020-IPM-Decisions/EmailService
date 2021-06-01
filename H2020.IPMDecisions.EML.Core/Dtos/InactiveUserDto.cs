using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class InactiveUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }
        [Required]
        public int InactiveMonths { get; set; }
        [Required]
        public string AccountDeletionDate { get; set; }
    }
}