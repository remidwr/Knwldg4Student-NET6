using Newtonsoft.Json;

namespace Application.Common.ExternalApi.Auth0Api
{
    public class UserCreationResponse
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }
    }
}