namespace Application.Features.RoleFeatures.Queries.GetRoles
{
    public class RolesVm
    {
        public RolesVm()
        {
            Roles = new List<RoleDto>();
        }

        public RolesVm(IEnumerable<RoleDto> roles)
        {
            Roles = roles;
        }

        public IEnumerable<RoleDto> Roles { get; private set; }
    }
}