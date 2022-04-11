namespace Application.Features.SectionFeatures.Queries.GetSectionById
{
    public class SectionDetailedDto : IMapFrom<Section>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CourseDto> Courses { get; set; }
    }
}