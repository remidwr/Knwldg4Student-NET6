namespace Domain.AggregatesModel.SectionAggregate
{
    public class Section
        : Entity, IAggregateRoot
    {
        protected Section()
        {
            Courses = new List<Course>();
            Title = string.Empty;
        }

        public Section(string title)
            : this()
        {
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
        }

        public Section(string title, ICollection<Course> courses)
            : this(title)
        {
            Courses = courses;
        }

        public string Title { get; private set; }

        public ICollection<Course> Courses { get; private set; }

        public void OrderCoursesByLabel()
        {
            if (Courses != null && Courses.Count > 1)
            {
                Courses = Courses.OrderBy(c => c.Label).ToList();
            }
        }
    }
}