using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace H2020.IPMDecisions.EML.API.Filters
{
    public class LocationMiddleware
    {
        private readonly RequestDelegate next;
        public LocationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var requestReader = new StreamReader(request.Body);
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                var requestContent = Encoding.UTF8.GetString(buffer);
                var bodyAsObject = JsonConvert.DeserializeObject<EmailDto>(requestContent);
                var language = "";
                if (bodyAsObject != null)
                {
                    language = bodyAsObject.Language;
                }

                if (string.IsNullOrEmpty(language)) language = "en";

                var culture = new CultureInfo(language);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                request.Body.Position = 0;
                await next(context);
            }
            catch (Exception)
            {
                context.Request.Body.Position = 0;
                context.Abort();
                return;
            }
        }
    }
}