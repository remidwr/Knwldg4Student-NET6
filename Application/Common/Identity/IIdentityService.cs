namespace Application.Common.Identity
{
    public interface IIdentityService
    {
        string GetUserId();

        string GetUserIdentity();

        string GetUserName();
    }
}