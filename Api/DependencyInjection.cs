using Api.Configurations;
using Api.Infrastructure.Services;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                          });
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

            services.AddSwaggerGen()
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

                config.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("api-version"),
                    new UrlSegmentApiVersionReader(),
                    new MediaTypeApiVersionReader("ver"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static void UseCustomSwagger(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, req) =>
                {
                    swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
                });
            });

            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
                    options.DefaultModelsExpandDepth(-1);
                    options.DocExpansion(DocExpansion.None);
                }
            });
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