using Application.Features.StudentFeatures.Commands.CreateStudent;

namespace Application.Common.ExternalApi.Auth0Api
{
    public interface IAuth0Api
    {
        Task<TokenResponse> GetTokenAsync();

        Task<UserCreationResponse> CreateUserAsync(CreateStudentCommand student);
    }
}