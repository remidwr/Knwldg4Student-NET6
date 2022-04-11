namespace Application.Features.MeetingFeatures.Queries.GetMeetingById
{
    public class MeetingStudentDto : IMapFrom<Student>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}