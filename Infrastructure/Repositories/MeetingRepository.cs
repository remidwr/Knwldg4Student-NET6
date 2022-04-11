using Domain.Exceptions;

namespace Infrastructure.Repositories
{
    public class MeetingRepository
        : IMeetingRepository
    {
        private readonly KnwldgContext _context;

        public MeetingRepository(KnwldgContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Meeting>> GetMeetingsByStudentIdAsync(int studentId)
        {
            var meetings = await _context.Meetings
                .Include(m => m.Course)
                .Include(m => m.MeetingStatus)
                .Where(m => m.StudentMeetings.Any(mg => mg.StudentId == studentId))
                .ToListAsync();

            return meetings;
        }

        public async Task<IEnumerable<Meeting>> GetJointMeetingsBetweenStudentsAsync(int firstStudentId, int secondStudentId)
        {
            var meetingIds = await _context.StudentMeetings
                .Where(sm => sm.StudentId == firstStudentId
                    || sm.StudentId == secondStudentId)
                .GroupBy(sm => sm.MeetingId)
                .Where(sm => sm.Count() > 1)
                .Select(sm => sm.Key)
                .ToListAsync();

            var meetings = await _context.Meetings
                .Include(m => m.MeetingStatus)
                .Where(m => meetingIds.Contains(m.Id))
                .ToListAsync();

            return meetings;
        }

        public async Task<Meeting> GetByIdAsync(int id)
        {
            var meeting = await _context.Meetings
                .Include(m => m.Course)
                .Include(m => m.MeetingStatus)
                .Include(m => m.StudentMeetings)
                    .ThenInclude(mg => mg.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meeting == null)
            {
                throw new KnwldgDomainException($"Meeting not found with id \"{id}\"");
            }

            return meeting;
        }

        public Meeting Add(Meeting meeting)
        {
            if (meeting.IsTransient())
            {
                return _context.Meetings
                    .Add(meeting)
                    .Entity;
            }
            else
            {
                return meeting;
            }
        }

        public Meeting Update(Meeting meeting)
        {
            return _context.Meetings
                .Update(meeting)
                .Entity;
        }

        public void AddInstructor(StudentMeeting instructorToAdd)
        {
            _context.StudentMeetings.Add(instructorToAdd);
        }

        public void AddTrainees(IList<StudentMeeting> traineesToAdd)
        {
            _context.StudentMeetings.AddRange(traineesToAdd);
        }
    }
}