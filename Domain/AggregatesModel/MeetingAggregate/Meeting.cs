namespace Domain.AggregatesModel.MeetingAggregate
{
    public partial class Meeting
        : Entity, IAggregateRoot
    {
        protected Meeting()
        {
            StudentMeetings = new List<StudentMeeting>();
        }

        public Meeting(string title, int courseId, DateTime startAt, DateTime endAt, string? description = null)
            : this()
        {
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new KnwldgDomainException(nameof(title));
            CourseId = courseId;

            if (startAt < DateTime.Now || startAt >= endAt)
            {
                throw new KnwldgDomainException($"The dates (startAt: {startAt:dd/MM/yyyy HH:mm:ss} - endAt: {endAt:dd/MM/yyyy HH:mm:ss}) for the meeting or not correct");
            }

            StartAt = startAt;
            EndAt = endAt;
            Description = description;
            MeetingStatusId = MeetingStatus.Sent.Id;
        }

        public string Title { get; private set; }
        public DateTime StartAt { get; private set; }
        public DateTime EndAt { get; private set; }
        public string? Description { get; private set; }
        public int CourseId { get; private set; }
        public int MeetingStatusId { get; private set; }

        public Course Course { get; private set; }
        public MeetingStatus MeetingStatus { get; private set; }
        public ICollection<StudentMeeting> StudentMeetings { get; private set; }

        public StudentMeeting AddInstructor(Meeting meeting, int studentId)
        {
            var studentMeeting = new StudentMeeting(studentId, meeting, isInstructor: true);
            StudentMeetings.Add(studentMeeting);

            return studentMeeting;
        }

        public List<StudentMeeting> AddTrainees(Meeting meeting, IList<int> studentIds)
        {
            foreach (var studentId in studentIds)
            {
                var studentMeeting = new StudentMeeting(studentId, meeting, isInstructor: false);
                StudentMeetings.Add(studentMeeting);
            }

            return StudentMeetings.ToList();
        }
    }
}