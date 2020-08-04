using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class DataShareDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }
        [Required]
        public string DataRequesterName { get; set; }
    }
}