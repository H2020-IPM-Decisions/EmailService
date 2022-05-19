using H2020.IPMDecisions.EML.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.BLL;
using AutoMapper;
using H2020.IPMDecisions.EML.Core.Profiles;
using H2020.IPMDecisions.EML.BLL.Providers;
using H2020.IPMDecisions.EML.API.Filters;

namespace H2020.IPMDecisions.EML.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (!CurrentEnvironment.IsDevelopment())
            {
                services.ConfigureHttps(Configuration);
            }

            services.ConfigureKestrelWebServer(Configuration);

            services.ConfigureCors(Configuration);
            services.ConfigureContentNegotiation();
            services.ConfigureJwtAuthentication(Configuration);
            services.ConfigureEmailSettings(Configuration);

            services.AddAutoMapper(typeof(MainProfile));
            services.ConfigureLogger(Configuration);

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IMarketingEmailingList, SendGridMarketingEmailingList>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IJsonStringLocalizer, JsonStringLocalizer>();
            services.AddSingleton<IJsonStringLocalizerProvider, JsonStringLocalizerProvider>();
            services.AddScoped<IBusinessLogic, BusinessLogic>();

            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                if (CurrentEnvironment.IsProduction())
                {
                    app.UseForwardedHeaders();
                    app.UseHsts();
                    app.UseHttpsRedirection();
                }
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error happened. Try again later.");
                    });
                });
            }

            app.UseCors("EmailServiceCORS");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<LocationMiddleware>();

            var swaggerBasePath = Configuration["MicroserviceInternalCommunication:EmailMicroservice"];
            app.UseSwagger(c =>
            {
                c.RouteTemplate = swaggerBasePath + "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{swaggerBasePath}swagger/v1/swagger.json", "H2020 IPM Decisions - Email Service API");
                c.RoutePrefix = $"{swaggerBasePath}swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
