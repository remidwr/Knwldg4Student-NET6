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

            return services;
        }
    }
}