using Application.Common.Identity;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Versioning;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using WebUI.Infrastructure.Identity;

namespace WebUI
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

        public static void AddAuth0RegularWebConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSameSiteNoneCookies();

            services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = configuration["Auth0Setting:Domain"];
                options.ClientId = configuration["Auth0Setting:ClientId"];
                options.CallbackPath = new PathString("/callback");
            });
        }

        private static IServiceCollection ConfigureSameSiteNoneCookies(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
            });

            return services;
        }

        private static void CheckSameSite(CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None && options.Secure == false)
            {
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}