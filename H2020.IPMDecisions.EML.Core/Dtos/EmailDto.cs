using System.ComponentModel.DataAnnotations;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.Core.Dtos
{
    public class EmailDto : ILocationEmailTemplate
    {
        [Required]
        [DataType(DataType.EmailAddress)] public string ToAddress { get; set; }
        public string Token { get; set; }
        public string Language { get; set; } = "en";

        public TranslatedEmailBodyParts TranslatedEmailBodyParts { get; set; }
        public TranslatedSharedEmailParts TranslatedSharedEmailParts { get; set; }
    }
}