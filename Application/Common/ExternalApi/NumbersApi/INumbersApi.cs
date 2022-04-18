using Application.Features.NumbersApiFeatures.Queries.GetDateFact;

namespace Application.Common.ExternalApi.NumbersApi
{
    public interface INumbersApi
    {
        Task<DateFactResponse> GetDateFactAsync();
    }
}