namespace Domain.AggregatesModel.MeetingAggregate
{
    public class MeetingStatus
        : Enumeration
    {
        public static MeetingStatus Sent = new(1, nameof(Sent).ToLowerInvariant());
        public static MeetingStatus Confirmed = new(2, nameof(Confirmed).ToLowerInvariant());
        public static MeetingStatus Cancelled = new(3, nameof(Cancelled).ToLowerInvariant());
        public static MeetingStatus Completed = new(4, nameof(Completed).ToLowerInvariant());

        public MeetingStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<MeetingStatus> List() =>
            new[] { Sent, Confirmed, Cancelled, Completed };

        public static MeetingStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new KnwldgDomainException($"Possible values for MeetingStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MeetingStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new KnwldgDomainException($"Possible values for MeetingStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}