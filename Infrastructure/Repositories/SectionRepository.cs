namespace Infrastructure.Repositories
{
    public class SectionRepository
        : ISectionRepository
    {
        private readonly KnwldgContext _context;

        public SectionRepository(KnwldgContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Section>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sections = await _context.Sections
                .Include(s => s.Courses)
                .OrderBy(s => s.Title)
                .ToListAsync(cancellationToken);

            return sections;
        }

        public async Task<Section> GetSectionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var section = await _context.Sections
                .Include(s => s.Courses)
                .Where(s => s.Id == id)
                .Select(s => new Section(
                    s.Title,
                    s.Courses.OrderBy(c => c.Label).ToList()))
                .FirstOrDefaultAsync(cancellationToken);

            return section;
        }

        public async Task<IEnumerable<Course>> GetCoursesBySectionIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var courses = await _context.Sections
                                        .Include(s => s.Courses)
                                        .Where(s => s.Id == id)
                                        .SelectMany(s => s.Courses)
                                        .ToListAsync(cancellationToken);

            return courses;
        }
    }
}