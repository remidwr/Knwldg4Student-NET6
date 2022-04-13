using Api.Infrastructure.Services;

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
                    Title = "Knwldg4Student API",
                    Version = "v1",
                    Description = "Knlwldg4Student API"
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                options.IncludeXmlComments(filePath);

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}