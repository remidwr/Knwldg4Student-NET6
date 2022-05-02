namespace Infrastructure.ExternalApi.NumbersApi
{
    public class Endpoints
    {
        public class GetDateFact
        {
            private readonly string _getDateFactPath;

            public GetDateFact(IConfiguration configuration)
            {
                _getDateFactPath = configuration["NumbersApi:GetDateFactPath"];
            }

            public string GetDateFacts => string.Format(_getDateFactPath, DateTime.Now.Month, DateTime.Now.Day)
        }
    }
}