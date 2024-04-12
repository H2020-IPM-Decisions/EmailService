using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace H2020.IPMDecisions.EML.Core.Models
{
    public class ReportDataFarm
    {
        public ReportDataFarm()
        {
            DssModels = new List<ReportDataDssModel>();
        }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("dssModels")]
        public List<ReportDataDssModel> DssModels { get; set; }
    }

    public class ReportDataDssModel
    {
        [JsonPropertyName("modelName")]
        public string ModelName { get; set; }
        [JsonPropertyName("modelId")]
        public string ModelId { get; set; }
    }
}