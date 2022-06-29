using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class InternalReportDto
    {
        [Required]
        public string ToAddresses { get; set; }

        [Required]
        public string ReportData { get; set; }
    }
}