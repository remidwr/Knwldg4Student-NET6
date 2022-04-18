using Application.Common.ExternalApi.NumbersApi;

namespace Application.Features.NumbersApiFeatures.Queries.GetDateFact
{
    public class DateFactDto : IMapFrom<DateFactResponse>
    {
        public string Text { get; set; }
        public string Year { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DateFactResponse, DateFactDto>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(df => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{df.Year}"));
        }
    }
}