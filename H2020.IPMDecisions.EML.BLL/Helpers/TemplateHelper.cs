using System.Reflection;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.EmailTemplates;
using RazorLight;

namespace H2020.IPMDecisions.EML.BLL.Helpers
{
    public static class TemplateHelper
    {
        public static async Task<string> GetEmbeddedTemplateHtmlAsStringAsync<T>(string templateFolderPath, T model)
        {
            var assembly = typeof(EmailTemplates).Assembly;

            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(assembly)
                .UseMemoryCachingProvider()
                .Build();

            return await engine.CompileRenderAsync(GenerateFileAssemblyPath(assembly, templateFolderPath), model);
        }

        private static string GenerateFileAssemblyPath(Assembly assembly,string templateFolderPath)
        {
            string assemblyName = assembly.GetName().Name;
            return string.Format("{0}.{1}.{2}", assemblyName, templateFolderPath, "cshtml");
        }
    }
}