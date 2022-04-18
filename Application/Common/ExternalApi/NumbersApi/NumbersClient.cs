using Flurl;

namespace Application.Common.ExternalApi.NumbersApi
{
    public class NumbersClient : BaseHttpClient
    {
        private readonly string _baseUri;
        private readonly string _getDateFactPath;

        public NumbersClient(HttpClient httpClient,
            IConfiguration configuration)
            : base(httpClient)
        {
            _baseUri = configuration["NumbersApi:BaseUrl"];
            _getDateFactPath = configuration["NumbersApi:GetDateFactPath"];
        }

        public async Task<DateFactResponse> GetDateFactAsync()
        {
            var getDateFactPath = string.Format(_getDateFactPath, DateTime.Now.Month, DateTime.Now.Day);
            var dateFactUri = Url.Combine(_baseUri, getDateFactPath);

            return await Get<DateFactResponse>(dateFactUri);
        }
    }
}