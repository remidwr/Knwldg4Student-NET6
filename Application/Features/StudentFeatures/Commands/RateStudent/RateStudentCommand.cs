namespace Application.Features.StudentFeatures.Commands.RateStudent
{
    public class RateStudentCommand : IRequest<int>
    {
        public int StudentId { get; set; }
        public decimal Stars { get; set; }
        public string? Comment { get; set; }
        public int ReviewerId { get; set; } //TODO Get by identity service (token)
    }

    public class RateStudentCommandHandler : IRequestHandler<RateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMeetingRepository _meetingRepository;

        public RateStudentCommandHandler(IStudentRepository studentRepository,
            IMeetingRepository meetingRepository)
        {
            _studentRepository = studentRepository;
            _meetingRepository = meetingRepository;
        }

        public async Task<int> Handle(RateStudentCommand command, CancellationToken cancellationToken)
        {
            var existingMeetings = await _meetingRepository.GetJointMeetingsBetweenStudentsAsync(command.StudentId, command.ReviewerId);
            var hasCompletedMeeting = HasOneCompletedMeeting(existingMeetings);

            if (!hasCompletedMeeting)
            {
                throw new NotFoundException("At least one joint meeting has to be completed before rating this student.");
            }

            var reviewerFullName = await GetReviewerFullNameAsync(command.ReviewerId);

            var rating = new Rating(command.StudentId, command.Stars, command.Comment, reviewerFullName);
            _studentRepository.AddRating(rating);

            return await _studentRepository.UnitOfWork.SaveChangesAsync();
        }

        private static bool HasOneCompletedMeeting(IEnumerable<Meeting> existingMeetings)
        {
            return existingMeetings
                .Where(m => m.MeetingStatusId == MeetingStatus.Completed.Id)
                .Any();
        }

        private async Task<string> GetReviewerFullNameAsync(int reviewerId)
        {
            var reviewer = await _studentRepository.GetByIdAsync(reviewerId);

            if (reviewer == null)
            {
                throw new NotFoundException(nameof(Student), reviewerId);
            }

            var reviewerFullName = $"{reviewer.FirstName} {reviewer.LastName}";
            return reviewerFullName;
        }
    }
}