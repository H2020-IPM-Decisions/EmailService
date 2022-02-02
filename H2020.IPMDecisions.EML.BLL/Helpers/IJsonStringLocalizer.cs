using Microsoft.Extensions.Localization;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public interface IJsonStringLocalizer
    {
        LocalizedString this[string name, string language] { get; }
        LocalizedString this[string name, string language, params object[] arguments] { get; }
    }
}