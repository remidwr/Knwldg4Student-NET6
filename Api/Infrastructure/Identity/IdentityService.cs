using System.Security.Claims;

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
            return _context.HttpContext.User.Claims.First(x => x.Type.Equals("sub")).Value;
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext.User.Claims.First(x => x.Type.EndsWith("nameidentifier")).Value;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}