namespace Application.Features.StudentFeatures.Queries.GetStudentById
{
    public class RatingDto : IMapFrom<Rating>
    {
        public int Id { get; set; }
        public double Stars { get; set; }
        public string Comment { get; set; }
        public string RatedBy { get; set; }
    }
}