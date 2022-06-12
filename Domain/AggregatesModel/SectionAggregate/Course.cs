namespace Domain.AggregatesModel.SectionAggregate
{
    public class Course
        : Entity
    {
        protected Course()
        {
            Students = new List<Student>();
        }

        public Course(int sectionId, string label)
            : this()
        {
            Label = !string.IsNullOrWhiteSpace(label) ? label : throw new ArgumentNullException(nameof(label));
            SectionId = sectionId;
        }

        public string Label { get; private set; }
        public int SectionId { get; private set; }

        public Section Section { get; private set; }
        public ICollection<Student> Students { get; private set; }
    }
}