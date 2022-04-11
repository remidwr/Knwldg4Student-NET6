namespace Application.Features.StudentFeatures.Queries.GetStudentById
{
    public class StudentDetailedDto : IMapFrom<Student>
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Description { get; set; }
        public IList<RatingDto> Ratings { get; set; }
        public IList<CourseDto> Courses { get; set; }
        public IList<UnavailableDayDto> UnavailableDays { get; set; }
        public IList<MeetingDto> Meetings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDetailedDto>()
                .ForMember(dest => dest.Meetings, opt => opt.MapFrom(s =>
                    s.StudentMeetings
                        .Where(sm => sm.StudentId == s.Id)
                        .Select(sm => sm.Meeting)));
        }
    }
}