namespace Infrastructure.Repositories
{
    public class StudentRepository
        : IStudentRepository
    {
        private readonly KnwldgContext _context;

        public StudentRepository(KnwldgContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = await _context.Students
                .Include(s => s.Ratings)
                .ToListAsync();

            return students;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.Ratings)
                .Include(s => s.UnavailableDays)
                .Include(s => s.Courses)
                .Include(s => s.StudentMeetings)
                    .ThenInclude(sm => sm.Meeting)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public Student Add(Student student)
        {
            if (student.IsTransient())
            {
                return _context.Students
                    .Add(student)
                    .Entity;
            }
            else
            {
                return student;
            }
        }

        public Student Update(Student student)
        {
            return _context.Students
                .Update(student)
                .Entity;
        }

        public Task<bool> HasUniqueEmailAsync(string email)
        {
            return _context.Students
                .AllAsync(s => s.Email != email);
        }

        public Rating AddRating(Rating rating)
        {
            if (rating.IsTransient())
            {
                return _context.Ratings
                    .Add(rating)
                    .Entity;
            }
            else
            {
                return rating;
            }
        }
    }
}