namespace Application.Common.Handler
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var permissions = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);

            if (permissions.Any(s => s.Value == requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}