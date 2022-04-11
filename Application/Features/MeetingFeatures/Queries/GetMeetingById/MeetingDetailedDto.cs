namespace Application.Features.MeetingFeatures.Queries.GetMeetingById
{
    public class MeetingDetailedDto : IMapFrom<Meeting>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CourseName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string StatusName { get; set; }
        public string? Description { get; set; }
        public MeetingStudentDto Instructor { get; set; }
        public IList<MeetingStudentDto> Trainees { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meeting, MeetingDetailedDto>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(m => m.MeetingStatus.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(m => m.Course.Label))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(m =>
                    m.StudentMeetings
                        .First(mg => mg.IsInstructor).Student))
                .ForMember(dest => dest.Trainees, opt => opt.MapFrom(m =>
                    m.StudentMeetings
                        .Where(mg => !mg.IsInstructor)
                        .Select(mg => mg.Student)));
        }
    }
}