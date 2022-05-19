using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H2020.IPMDecisions.EML.BLL.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;

namespace H2020.IPMDecisions.EML.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSecretKey = config["JwtSettings:SecretKey"];
            var authorizationServerUrl = config["JwtSettings:IssuerServerUrl"];
            var audiencesServerUrl = Audiences(config["JwtSettings:ValidAudiences"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = authorizationServerUrl,
                    ValidAudiences = audiencesServerUrl,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                };
            });
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            var allowedHosts = config["AllowedHosts"];
            if (allowedHosts == null) return;

            services.AddCors(options =>
            {
                options.AddPolicy("EmailServiceCORS", builder =>
                {
                    builder.WithOrigins(allowedHosts);
                });
            });
        }

        public static void ConfigureContentNegotiation(this IServiceCollection services)
        {
            services.AddControllers(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                })
            .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "H2020 IPM Decisions - Email Service API",
                    Version = "v1",
                    Description = "Email Service for the H2020 IPM Decisions project",
                    // TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "ADAS Modelling and Informatics Team",
                        Email = "software@adas.co.uk",
                        Url = new Uri("https://www.adas.uk/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GNU General Public License v3.0",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.txt"),
                    }
                });
                c.DescribeAllParametersInCamelCase();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                });

            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void ConfigureKestrelWebServer(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KestrelServerOptions>(
                config.GetSection("Kestrel")
            );
        }

        public static void ConfigureHttps(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = int.Parse(config["ASPNETCORE_HTTPS_PORT"]);
            });
        }

        public static void ConfigureEmailSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailSettingsProvider>(options =>{
                options.SmtpServer = config["EmailSettings:SmtpServer"];
                options.SmtpPort = int.Parse(config["EmailSettings:SmtpPort"]);
                options.SmtpUsername = config["EmailSettings:SmtpUsername"];
                options.SmtpPassword = config["EmailSettings:SmtpPassword"];
                options.FromAddress = config["EmailSettings:FromAddress"];
                options.FromName = config["EmailSettings:FromName"];
                options.EnableSsl = bool.Parse(config["EmailSettings:EnableSsl"]);
                options.UseSmtpLoginCredentials = bool.Parse(config["EmailSettings:UseSmtpLoginCredentials"]);
            });
        }

        internal static void ConfigureLogger(this IServiceCollection services, IConfiguration config)
        {
            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
        }

        private static IEnumerable<string> Audiences(string audiences)
        {
            var listOfAudiences = new List<string>();
            if (string.IsNullOrEmpty(audiences)) return listOfAudiences;
            listOfAudiences = audiences.Split(';').ToList();
            return listOfAudiences;
        }
    }
}