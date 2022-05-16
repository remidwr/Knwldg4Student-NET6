using Application.Common.ExternalApi.Auth0Api;
using Application.Features.StudentFeatures.Commands.CreateStudent;

namespace Infrastructure.ExternalApi.Auth0Api
{
    public class Auth0Api : IAuth0Api
    {
        private readonly Auth0Client _client;

        public Auth0Api(Auth0Client client)
        {
            _client = client;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            return await _client.GetTokenAsync();
        }

        public async Task<UserCreationResponse> CreateUserAsync(CreateStudentCommand command)
        {
            return await _client.CreateUserAsync(command);
        }
    }
}