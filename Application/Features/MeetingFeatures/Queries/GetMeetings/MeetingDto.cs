namespace Application.Features.MeetingFeatures.Queries.GetMeetings
{
    public class MeetingDto : IMapFrom<Meeting>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string CourseName { get; set; }
        public string StatusName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meeting, MeetingDto>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(m => m.Course.Label))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(m => m.MeetingStatus.Name));
        }
    }
}