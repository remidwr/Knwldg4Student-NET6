namespace Domain.AggregatesModel.StudentAggregate
{
    public class StudentMeeting
    {
        public StudentMeeting()
        {
            HasConfirmed = false;
        }

        public StudentMeeting(int studentId, Meeting meeting, bool isInstructor)
            : this()
        {
            Meeting = meeting;
            StudentId = studentId;
            IsInstructor = isInstructor;
        }

        public int MeetingId { get; private set; }
        public int StudentId { get; private set; }
        public bool IsInstructor { get; private set; }
        public bool HasConfirmed { get; private set; }

        public Meeting Meeting { get; private set; }
        public Student Student { get; private set; }
    }
}