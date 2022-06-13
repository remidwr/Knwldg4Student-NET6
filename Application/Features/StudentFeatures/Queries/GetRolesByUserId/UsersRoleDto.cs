using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.StudentFeatures.Queries.GetRolesByUserId
{
    public class UsersRoleDto : IMapFrom<UsersRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}