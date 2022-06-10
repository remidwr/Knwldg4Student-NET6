using Application.Common.ExternalApi.Auth0Api;
using Application.Common.ExternalApi.NumbersApi;
using Application.Common.ExternalApi.UdemyApi;

using Flurl;

using Infrastructure.ExternalApi.Auth0Api;
using Infrastructure.ExternalApi.NumbersApi;
using Infrastructure.ExternalApi.UdemyApi;
using Infrastructure.Repositories;

using Polly;
using Polly.Extensions.Http;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KnwldgContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(KnwldgContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<KnwldgContext>());
            services.AddRepositories();
            services.AddAuth0ApiConfiguration(configuration);
            services.AddNumbersApiConfiguration(configuration);
            services.AddUdemyApiConfiguration();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();

            return services;
        }

        private static IServiceCollection AddUdemyApiConfiguration(this IServiceCollection services)
        {
            services.AddHttpClient<UdemyClient>("UdemyClient", config =>
            {
                config.BaseAddress = new Uri("https://www.udemy.com");
                config.Timeout = TimeSpan.FromSeconds(30);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddTransient<IUdemyApi, UdemyApi>();

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
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddTransient<INumbersApi, NumbersApi>();

            return services;
        }

        private static IServiceCollection AddAuth0ApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<Auth0Client>("Auth0Client", config =>
            {
                config.BaseAddress = new Uri($"https://{configuration["Auth0Setting:Domain"]}");
                config.Timeout = TimeSpan.FromSeconds(30);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddTransient<IAuth0Api, Auth0Api>();

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}