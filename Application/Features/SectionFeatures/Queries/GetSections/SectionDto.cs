namespace Application.Features.SectionFeatures.Queries.GetSections
{
    public class SectionDto : IMapFrom<Section>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<CourseDto> Courses { get; set; }
    }
}