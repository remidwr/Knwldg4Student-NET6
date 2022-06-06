using Application.Common.Identity;

namespace Application.Features.MeetingFeatures.Commands.CreateMeeting
{
    public class CreateMeetingCommand : IRequest<int>
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string? Description { get; set; }
        //public IList<int> TraineeIds { get; set; }
    }

    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IIdentityService _identityService;

        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository,
            IIdentityService identityService,
            IStudentRepository studentRepository)
        {
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        public async Task<int> Handle(CreateMeetingCommand command, CancellationToken cancellationToken)
        {
            var meeting = new Meeting(command.Title, command.CourseId, command.StartAt, command.EndAt, command.Description);
            _meetingRepository.Add(meeting);

            var instructorToAdd = meeting.AddInstructor(meeting, command.InstructorId);
            _meetingRepository.AddInstructor(instructorToAdd);

            var traineeIds = await GetCurrentTraineeIdsAsync();
            var traineesToAdd = meeting.AddTrainees(meeting, traineeIds /*command.TraineeIds*/);
            _meetingRepository.AddTrainees(traineesToAdd);

            return await _meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task<List<int>> GetCurrentTraineeIdsAsync()
        {
            var userId = _identityService.GetUserId();

            var trainee = await _studentRepository.GetByIdAsync(userId);

            if (trainee == null)
                throw new NotFoundException(nameof(Student), userId);

            var traineeIds = new List<int>
            {
                trainee.Id
            };

            return traineeIds;
        }
    }
}