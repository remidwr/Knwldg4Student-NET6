namespace Application.Features.SectionFeatures.Queries.GetSectionById
{
    public class CourseDto : IMapFrom<Course>
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}