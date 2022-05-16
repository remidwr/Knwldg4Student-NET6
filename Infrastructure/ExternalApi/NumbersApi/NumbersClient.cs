using Application.Common.ExternalApi.NumbersApi;

using Flurl;

namespace Infrastructure.ExternalApi.NumbersApi
{
    public class NumbersClient : BaseHttpClient
    {
        private readonly string _baseUri;
        private readonly string _getDateFactPath;
        private readonly IConfiguration _configuration;

        public NumbersClient(HttpClient httpClient,
            IConfiguration configuration)
            : base(httpClient)
        {
            _configuration = configuration;
            _baseUri = configuration["NumbersApi:BaseUrl"];
            _getDateFactPath = configuration["NumbersApi:GetDateFactPath"];
        }

        public async Task<DateFactResponse> GetDateFactAsync()
        {
            var getDateFactPath = string.Format(/*_getDateFactPath*/_configuration["NumbersApi:GetDateFactPath"], DateTime.Now.Month, DateTime.Now.Day);
            var dateFactUri = Url.Combine(_baseUri, getDateFactPath);

            return await GetAsync<DateFactResponse>(dateFactUri);
        }
    }
}