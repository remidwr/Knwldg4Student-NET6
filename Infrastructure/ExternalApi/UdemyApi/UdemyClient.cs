using System.Net.Http.Headers;
using System.Text;

using Application.Common.ExternalApi.UdemyApi;

using Newtonsoft.Json;

namespace Infrastructure.ExternalApi.UdemyApi
{
    public class UdemyClient : BaseHttpClient
    {
        private readonly IConfiguration _configuration;

        public UdemyClient(HttpClient httpClient,
            IConfiguration configuration)
            : base(httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<CourseListResponse> GetCoursesAsync(string search, int pageNumber = 1, int pageSize = 50, string ordering = null)
        {
            if (ordering == null || string.IsNullOrWhiteSpace(ordering))
                ordering = "";

            if (search == null || string.IsNullOrWhiteSpace(search))
                search = "";

            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"/api-2.0/courses/?page={pageNumber}&page_size={pageSize}&search={search}&ordering={ordering}");
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = string.IsNullOrEmpty(responseContent)
                            ? default
                            : JsonConvert.DeserializeObject<CourseListResponse>(responseContent);

            return result;
        }

        private void SetAuthorizationHeader()
        {
            var base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_configuration["UdemyApi:ClientId"]}:{_configuration["UdemyApi:ClientSecret"]}"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Authorization);
        }
    }
}