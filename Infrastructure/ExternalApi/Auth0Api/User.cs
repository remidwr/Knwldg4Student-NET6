using Newtonsoft.Json;

namespace Infrastructure.ExternalApi.Auth0Api
{
    public class User
    {
        public User(string email,
            string nickname,
            string password)
        {
            Email = email;
            Nickname = nickname;
            Password = password;
            Connection = "Username-Password-Authentication";
        }

        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("nickname")]
        public string Nickname { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }

        [JsonProperty("connection")]
        public string Connection { get; private set; }
    }
}