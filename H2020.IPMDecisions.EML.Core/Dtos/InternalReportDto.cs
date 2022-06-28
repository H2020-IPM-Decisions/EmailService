using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class InternalReportDto
    {
        [Required]
        public string ToAddresses { get; set; }
    }
}