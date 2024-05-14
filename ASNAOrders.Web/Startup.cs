/*
 * API для интеграции сервиса Яндекс.Еда
 *
 * Описание API для работы с сервисом Яндекс.Еда по моделям \"Доставка Яндекс Едой\",  \"Доставка партнером\" и \"Самовывоз\". Все методы описанные ниже должны быть реализованы на стороне партнера в процессе интеграции. Т.е. сервис Яндекс.Еда выступает в роли клиента, а Вам необходимо реализовать серверную часть. Взаимодействие происходит на основании pull-модели, т.е. сервис Яндекс Еда как клиент запрашивает данные либо может создавать/обновлять данные если это необходимо.  # Authentication  <!- - ReDoc-Inject: <security-definitions> - ->
 *
 * The version of the OpenAPI document: 1.0-oas3
 * 
 * Generated by: https://openapi-generator.tech
 */

using ASNAOrders.Web.LogicServices;
using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ASNAOrders.Web.Authentication;
using ASNAOrders.Web.Filters;
using ASNAOrders.Web.OpenApi;
using ASNAOrders.Web.Formatters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Serilog;
using ASNAOrders.Web.ConfigServiceExtensions;
using Serilog.Sinks.Email;
using System.Net;
using ASNAOrders.Web.Data;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.PeriodicBatching;
using ASNAOrders.Web.Converters;
using ASNAOrders.Web.NotificationServiceExtensions;
using ASNAOrders.Web.OrdersServiceExtensions;

namespace ASNAOrders.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = StaticConfig.Sink.Contains("file") ? new LoggerConfiguration().WriteTo.File($"{StaticConfig.ErrorLogPrefix}.{StaticConfig.Sink.Split('*')[1]}").CreateLogger() :
                StaticConfig.Sink.Contains("mail") ? new LoggerConfiguration().WriteTo.Email(new EmailSinkOptions()
                {
                    From = StaticConfig.Sink.Split('*')[1],
                    ConnectionSecurity =
                    StaticConfig.MailSSLOptions == "auto" ? MailKit.Security.SecureSocketOptions.Auto
                    : StaticConfig.MailSSLOptions == "STARTTLSavail" ? MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable
                    : StaticConfig.MailSSLOptions == "SSL" ? MailKit.Security.SecureSocketOptions.SslOnConnect
                    : MailKit.Security.SecureSocketOptions.None,
                    To = new List<string>() { StaticConfig.MailTo },
                    Host = StaticConfig.MailHost,
                    Credentials = new NetworkCredential(StaticConfig.Sink.Split('*')[1], StaticConfig.MailPassword),
                    Port = (int)StaticConfig.MailPort

                },
                new PeriodicBatchingSinkOptions()
                {
                    BatchSizeLimit = int.Parse(Properties.Resources.MailBatchLimitString),
                    Period = new TimeSpan(0, int.Parse(Properties.Resources.MailBatchPeriodString), 0)

                }).CreateLogger() : new LoggerConfiguration().WriteTo.EventLog(source: Properties.Resources.EventLogSource, logName: Properties.Resources.EventLogName, manageEventSource: true).CreateLogger();

            services.AddSerilog();

            services.AddDbContextFactory<ASNAOrdersDbContext>(options =>
            {
                if (StaticConfig.DatabaseType == "mssqlserver") { options.UseLazyLoadingProxies().UseSqlServer(StaticConfig.ConnectionString); } else { options.UseLazyLoadingProxies().UseSqlite(StaticConfig.ConnectionString); };
            });

            services.AddSingleton<RabbitMQNotificationService>();
            services.AddSingleton<EntityModelConverter>();
            services.AddSingleton<RabbitMQOrdersService>();
            services.AddSingleton<DataFormattingService>();

            services.AddSingleton<IWatcherService, XMLStockWatcherService>();
            services.AddSingleton<IWatcherService, ImageWatcherService>();


            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddExceptionHandler<BadRequestErrorHandler>();

            services.AddSingleton<LogicServices.AuthorizationMiddleware>();

            // Add framework services.
            services
            // Don't need the full MVC stack for an API, see https://andrewlock.net/comparing-startup-between-the-asp-net-core-3-templates/
            .AddControllers(options =>
            {
                options.InputFormatters.Insert(0, new InputFormatterStream());
            })
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opts.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });
            });

            HttpContextExtensions.AddHttpContextAccessor(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretGeneratorService.GetIssuerSigningKey(StaticConfig.SigningKeyFileSetToAuto ? Properties.Resources.ConfigSigningKeyFile : StaticConfig.SigningKeyFile))
                };
            });

            services.AddAuthorization
            (
                options =>
                options.AddPolicy
                (
                    "ClientCredsReadWrite",
                    po =>
                    {
                        po.RequireClaim("grant_type", "client_credentials");
                        po.RequireClaim("scope", "read write");
                    }
                )
            );

            services
                .AddSwaggerGen(c =>
                {
                    OpenApiSecurityScheme s = new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.OAuth2,
                        Description = "Auth",
                        Scheme = "client_credentials",
                        BearerFormat = "JWT",
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri($"{ParametersHttpContext.ApplicationUrl}/security/oauth/token"),
                                Scopes = new Dictionary<string, string>()
                                {
                                    { "read", "Read access" },
                                    { "write", "Write access" }
                                },
                            }
                        }
                    };

                    c.AddSecurityDefinition("oauth2", s);

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { s, new List<string>() { "read" } },
                        { s, new List<string>() { "write" } }
                    });
                    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                    c.SwaggerDoc("1.0-oas3", new OpenApiInfo
                    {
                        Title = "API для интеграции сервиса Яндекс.Еда",
                        Description = "API для интеграции сервиса Яндекс.Еда (ASP.NET Core 3.1)",
                        TermsOfService = new Uri("https://yandex.ru/legal/eula"),
                        Contact = new OpenApiContact
                        {
                            Name = "Alexander Silitskiy",
                            Url = new Uri("https://github.com/alexander-sil"),
                            Email = ""
                        },
                        Version = "1.0-oas3",
                    });
                    c.CustomSchemaIds(type => type.FriendlyId(true));
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetEntryAssembly().GetName().Name}.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as OpenAPI validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });
            services
                .AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpContext();
            app.UseDefaultFiles();
            app.UseExceptionHandler(o => { });
            app.UseStaticFiles();

            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "openapi/{documentName}/openapi.json";
                })
                .UseSwaggerUI(c =>
                {
                    // set route prefix to openapi, e.g. http://localhost:8080/openapi/index.html
                    c.RoutePrefix = "openapi";
                    //TODO: Either use the SwaggerGen generated OpenAPI contract (generated from C# classes)
                    //c.SwaggerEndpoint("/openapi/1.0-oas3/openapi.json", "API для интеграции сервиса Яндекс.Еда");

                    //TODO: Or alternatively use the original OpenAPI contract that's included in the static files
                    c.SwaggerEndpoint("/openapi-original.json", "API для интеграции сервиса Яндекс.Еда Original");
                });

            app.UseMiddleware<LogicServices.AuthorizationMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }

}
