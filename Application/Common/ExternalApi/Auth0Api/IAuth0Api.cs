using Application.Features.StudentFeatures.Commands.AssignRolesToStudent;
using Application.Features.StudentFeatures.Commands.CreateStudent;

namespace Application.Common.ExternalApi.Auth0Api
{
    public interface IAuth0Api
    {
        Task<TokenResponse> GetTokenAsync();

        Task<UserCreationResponse> CreateUserAsync(CreateStudentCommand student);

        Task<IEnumerable<RoleResponse>> GetRolesAsync();

        Task<IEnumerable<UsersRole>> GetUsersRoleAsync(string id);

        Task AssignRolesToUserAsync(AssignRolesToStudentCommand command);
    }
}