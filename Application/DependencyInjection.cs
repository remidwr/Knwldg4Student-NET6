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

            return services;
        }

        private static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = $"https://{configuration["Auth0Setting:Domain"]}/";

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
                options.Audience = configuration["Auth0Setting:KnwldgApiAudience"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("view:student", policy => policy.Requirements.Add(new HasPermissionRequirement("view:student", domain)));
                options.AddPolicy("update:student", policy => policy.Requirements.Add(new HasPermissionRequirement("update:student", domain)));
                options.AddPolicy("create:assign-role", policy => policy.Requirements.Add(new HasPermissionRequirement("create:assign-role", domain)));
                options.AddPolicy("create:rate-student", policy => policy.Requirements.Add(new HasPermissionRequirement("create:rate-student", domain)));
                options.AddPolicy("create:student-dayoff", policy => policy.Requirements.Add(new HasPermissionRequirement("create:student-dayoff", domain)));
                options.AddPolicy("view:roles", policy => policy.Requirements.Add(new HasPermissionRequirement("view:roles", domain)));
                options.AddPolicy("view:meetings", policy => policy.Requirements.Add(new HasPermissionRequirement("view:meetings", domain)));
                options.AddPolicy("create:meetings", policy => policy.Requirements.Add(new HasPermissionRequirement("create:meetings", domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

            return services;
        }
    }
}