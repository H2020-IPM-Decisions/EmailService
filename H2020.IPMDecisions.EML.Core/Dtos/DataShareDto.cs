using System.ComponentModel.DataAnnotations;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class DataShareDto : EmailDto
    {
        [Required]
        public string DataRequesterName { get; set; }
    }
}