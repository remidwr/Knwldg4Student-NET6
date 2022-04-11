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
                .OrderBy(s => s.Title)
                .ToListAsync(cancellationToken);

            return sections;
        }

        public async Task<Section> GetSectionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var section = await _context.Sections
                .Include(s => s.Courses)
                .Where(s => s.Id == id)
                .OrderBy(s => s.Title)
                .FirstOrDefaultAsync(cancellationToken);

            section.OrderCoursesByLabel();

            return section;
        }
    }
}