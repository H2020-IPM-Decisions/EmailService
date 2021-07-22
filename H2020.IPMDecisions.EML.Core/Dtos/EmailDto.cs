using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class EmailDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }        
        public string Token { get; set; }
    }
}