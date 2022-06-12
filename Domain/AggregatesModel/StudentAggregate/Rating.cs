namespace Domain.AggregatesModel.StudentAggregate
{
    public class Rating
        : Entity
    {
        public Rating()
        {
        }

        public Rating(int studentId, decimal stars, string comment, string ratedBy)
            : this()
        {
            StudentId = studentId;

            if (stars < 0 && stars > 5)
            {
                throw new KnwldgDomainException($"The star(s) (stars: {stars}) must be between 0 and 5.");
            }

            Stars = stars;
            Comment = comment;
            RatedBy = !string.IsNullOrWhiteSpace(ratedBy) ? ratedBy : throw new ArgumentNullException(nameof(ratedBy));
        }

        public decimal Stars { get; private set; }
        public string? Comment { get; private set; }
        public string RatedBy { get; private set; }
        public int StudentId { get; private set; }

        public Student Student { get; private set; }
    }
}