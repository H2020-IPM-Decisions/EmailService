using System;
using H2020.IPMDecisions.EML.BLL.Helpers;

namespace H2020.IPMDecisions.EML.BLL.Providers
{
    public interface IJsonStringLocalizerProvider
    {
        IJsonStringLocalizer Create(Type resourceSource);
        IJsonStringLocalizer Create(string baseName, string location);
    }
}