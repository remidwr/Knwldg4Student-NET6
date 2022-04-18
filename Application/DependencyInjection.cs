using Application.Common.ExternalApi.NumbersApi;

using Flurl;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddAuth0(configuration);

            services.AddNumbersApiConfiguration(configuration);

            return services;
        }

        private static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = $"https://{configuration["Auth0:Domain"]}/";

            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.RequireHttpsMetadata = false;
                options.Audience = configuration["Auth0:Audience"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "read:messages")));
                options.AddPolicy("write:messages", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "write:messages")));
                options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "read:users")));
                options.AddPolicy("write:users", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "write:users")));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            return services;
        }

        private static IServiceCollection AddNumbersApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var now = DateTime.Now;

            var baseUrl = configuration["NumbersApi:BaseUrl"];
            var getDateFactPath = configuration["NumbersApi:GetDateFactPath"];

            getDateFactPath = string.Format(getDateFactPath, now.Month, now.Day);
            var getDateFactCompletePath = Url.Combine(baseUrl, getDateFactPath);

            services.AddHttpClient<NumbersClient>("NumbersClient", config =>
            {
                config.BaseAddress = new Uri(getDateFactCompletePath);
                config.DefaultRequestHeaders.Add("X-RapidAPI-Host", configuration["NumbersApi:X-RapidAPI-Host"]);
                config.DefaultRequestHeaders.Add("X-RapidAPI-Key", configuration["NumbersApi:X-RapidAPI-Key"]);
                config.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddTransient<INumbersApi, NumbersApi>();

            return services;
        }
    }
}