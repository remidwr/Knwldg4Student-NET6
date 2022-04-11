namespace Domain.AggregatesModel.MeetingAggregate
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<IEnumerable<Meeting>> GetMeetingsByStudentIdAsync(int studentId);

        Task<IEnumerable<Meeting>> GetJointMeetingsBetweenStudentsAsync(int firstStudentId, int secondStudentId);

        Task<Meeting> GetByIdAsync(int id);

        Meeting Add(Meeting meeting);

        Meeting Update(Meeting meeting);

        void AddInstructor(StudentMeeting studentMeeting);

        void AddTrainees(IList<StudentMeeting> studentMeeting);
    }
}