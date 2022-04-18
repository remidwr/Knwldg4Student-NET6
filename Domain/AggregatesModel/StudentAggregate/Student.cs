namespace Domain.AggregatesModel.StudentAggregate
{
    public class Student
        : Entity, IAggregateRoot
    {
        public Student()
        {
            Courses = new HashSet<Course>();
            StudentMeetings = new HashSet<StudentMeeting>();
            Ratings = new HashSet<Rating>();
            UnavailableDays = new HashSet<UnavailableDay>();
        }

        public Student(string firstName, string lastName, string email, string description)
            : this()
        {
            FirstName = firstName;
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
            Description = description;
        }

        public string ExternalId { get; private set; }
        public string? FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string? Description { get; private set; }

        public ICollection<Course> Courses { get; private set; }
        public ICollection<StudentMeeting> StudentMeetings { get; private set; }
        public ICollection<Rating> Ratings { get; private set; }
        public ICollection<UnavailableDay> UnavailableDays { get; private set; }

        public void UpdatePersonalInformations(string firstName, string lastName, string description)
        {
            FirstName = firstName;
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            Description = description;
        }

        public void AddRating(decimal stars, string comment, string ratedBy)
        {
            Ratings.Add(new Rating(Id, stars, comment, ratedBy));
        }
    }
}