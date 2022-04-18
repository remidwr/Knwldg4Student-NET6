namespace Application.Common.ExternalApi.NumbersApi
{
    public class NumbersApi : INumbersApi
    {
        private readonly NumbersClient _httpClient;

        public NumbersApi(NumbersClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DateFactResponse> GetDateFactAsync()
        {
            return await _httpClient.GetDateFactAsync();
        }
    }
}