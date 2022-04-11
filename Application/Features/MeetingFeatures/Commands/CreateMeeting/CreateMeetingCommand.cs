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
        public IList<int> TraineeIds { get; set; }
    }

    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IMeetingRepository _meetingRepository;

        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<int> Handle(CreateMeetingCommand command, CancellationToken cancellationToken)
        {
            var meeting = new Meeting(command.Title, command.CourseId, command.StartAt, command.EndAt, command.Description);
            _meetingRepository.Add(meeting);

            var instructorToAdd = meeting.AddInstructor(meeting, command.InstructorId);
            _meetingRepository.AddInstructor(instructorToAdd);

            var traineesToAdd = meeting.AddTrainees(meeting, command.TraineeIds);
            _meetingRepository.AddTrainees(traineesToAdd);

            return await _meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}