using Application.Common.Identity;

namespace Api.Infrastructure.Identity
{
    public class IdentityService : IIdentityService

    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            var sub = _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals("sub"));
            return sub?.Value;
        }

        public string GetUserIdentity()
        {
            var nameIdentifier = _context.HttpContext?.User.Claims.First(x => x.Type.EndsWith("nameidentifier"));
            return nameIdentifier?.Value;
        }

        public string GetUserName()
        {
            return _context.HttpContext?.User.Identity.Name;
        }
    }
}