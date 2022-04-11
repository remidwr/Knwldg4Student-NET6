namespace Application.Features.StudentFeatures.Queries.GetStudentById
{
    public class UnavailableDayDto : IMapFrom<UnavailableDay>
    {
        public int Id { get; set; }
        public DateTime DayOff { get; set; }
    }
}