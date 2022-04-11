namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddAuth0(configuration);

            return services;
        }

        private static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = $"https://{configuration["Auth0:Domain"]}/";

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.Authority = domain;
                option.Audience = configuration["Auth0:Audience"];
            });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "read:messages")));
                option.AddPolicy("write:messages", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "write:messages")));
                option.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "read:users")));
                option.AddPolicy("write:users", policy => policy.Requirements.Add(new HasScopeRequirement(domain, "write:users")));
            });

            return services;
        }
    }
}