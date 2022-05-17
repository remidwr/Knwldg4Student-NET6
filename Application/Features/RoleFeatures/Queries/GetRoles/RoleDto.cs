using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.RoleFeatures.Queries.GetRoles
{
    public class RoleDto : IMapFrom<RoleResponse>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}