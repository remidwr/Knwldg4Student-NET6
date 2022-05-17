using System.Net.Http.Headers;
using System.Text;

using Application.Common.ExternalApi.Auth0Api;
using Application.Features.StudentFeatures.Commands.AssignRolesToStudent;
using Application.Features.StudentFeatures.Commands.CreateStudent;

using Newtonsoft.Json;

namespace Infrastructure.ExternalApi.Auth0Api
{
    public class Auth0Client : BaseHttpClient
    {
        private readonly IConfiguration _configuration;

        public Auth0Client(HttpClient httpClient,
            IConfiguration configuration)
            : base(httpClient)
        {
            _configuration = configuration;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            var response = await _httpClient.PostAsync("/oauth/token", new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", _configuration["Auth0Setting:GrantType"] },
                    { "client_id", _configuration["Auth0Setting:ClientId"] },
                    { "client_secret", _configuration["Auth0Setting:ClientSecret"] },
                    { "audience", _configuration["Auth0Setting:ManagementApiAudience"] },
                }));

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = string.IsNullOrEmpty(responseContent)
                            ? default
                            : JsonConvert.DeserializeObject<TokenResponse>(responseContent);

            return result;
        }

        public async Task<UserCreationResponse> CreateUserAsync(CreateStudentCommand command)
        {
            var user = new User(command.Email, command.Username, command.Password);
            var body = JsonConvert.SerializeObject(user);

            await SetTokenToAuthenticationHeaderAsync();
            var response = await _httpClient.PostAsync("/api/v2/users", new StringContent(body, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = string.IsNullOrEmpty(responseContent)
                            ? default
                            : JsonConvert.DeserializeObject<UserCreationResponse>(responseContent);

            return result;
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesAsync()
        {
            await SetTokenToAuthenticationHeaderAsync();
            var response = await _httpClient.GetAsync("/api/v2/roles");
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = string.IsNullOrEmpty(responseContent)
                            ? default
                            : JsonConvert.DeserializeObject<IEnumerable<RoleResponse>>(responseContent);

            return result;
        }

        public async Task AssignRolesToUserAsync(AssignRolesToStudentCommand command)
        {
            var body = JsonConvert.SerializeObject(new { roles = command.RoleIds });

            await SetTokenToAuthenticationHeaderAsync();
            var response = await _httpClient.PostAsync($"/api/v2/users/{command.StudentExternalId}/roles", new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        private async Task SetTokenToAuthenticationHeaderAsync()
        {
            var tokenResponse = await GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.AccessToken);
        }
    }
}