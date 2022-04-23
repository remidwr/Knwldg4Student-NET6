using System.Reflection;

using Api.Infrastructure.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Knwldg4Student API",
                    Description = "Knlwldg4Student Web API to help students.",
                    Contact = new OpenApiContact
                    {
                        Name = "Rémi Dwr"
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            })
                .AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return services;
        }

        public static void UseSerilogConfiguration(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseSerilog((ctx, lc) => lc
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })));
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                options.ReturnHttpNotAcceptable = true;
            })
                .AddFluentValidation()
                .AddNewtonsoftJson(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    settings.Converters.Add(new StringEnumConverter());
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    settings.Formatting = Formatting.Indented;
                });
        }
    }
}