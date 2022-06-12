namespace Domain.AggregatesModel.StudentAggregate
{
    public class UnavailableDay
        : Entity
    {
        public UnavailableDay()
        {
            Recursive = false;
            Interval = 0;
        }

        public UnavailableDay(int studentId, DateTime dayOff, bool recursive, int interval)
            : this()
        {
            StudentId = studentId;
            DayOff = dayOff;
            Recursive = recursive;
            Interval = interval >= 0 ? interval : throw new KnwldgDomainException($"Interval (interval: {interval}) must be greater or equal than 0.");
        }

        public DateTime DayOff { get; private set; }
        public bool Recursive { get; private set; }
        public int Interval { get; private set; }
        public int StudentId { get; private set; }

        public Student Student { get; private set; }
    }
}