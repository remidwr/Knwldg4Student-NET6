using Application.Common.ExternalApi.NumbersApi;

namespace Application.Features.NumbersApiFeatures.Queries.GetDateFact
{
    public record GetDateFactQuery() : IRequest<DateFactDto>;

    public class GetDateFactRequestHandler : IRequestHandler<GetDateFactQuery, DateFactDto>
    {
        private readonly IMapper _mapper;
        private readonly INumbersApi _numbersApi;

        public GetDateFactRequestHandler(INumbersApi numbersApi,
            IMapper mapper)
        {
            _numbersApi = numbersApi;
            _mapper = mapper;
        }

        public async Task<DateFactDto> Handle(GetDateFactQuery request, CancellationToken cancellationToken)
        {
            var dateFactResponse = await _numbersApi.GetDateFactAsync();
            return _mapper.Map<DateFactDto>(dateFactResponse);
        }
    }
}