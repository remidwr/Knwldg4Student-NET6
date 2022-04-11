namespace Application.Features.SectionFeatures.Queries.GetSections
{
    public class SectionsVm
    {
        protected SectionsVm()
        {
            Sections = new List<SectionDto>();
        }

        public SectionsVm(IList<SectionDto> sections)
            : this()
        {
            Sections = sections;
        }

        public IList<SectionDto> Sections { get; private set; }
    }
}