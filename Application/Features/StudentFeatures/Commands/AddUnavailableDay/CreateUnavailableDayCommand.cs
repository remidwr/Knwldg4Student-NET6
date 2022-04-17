namespace Application.Features.StudentFeatures.Commands.AddUnavailableDay
{
    public class CreateUnavailableDayCommand : IRequest<int>
    {
        public DateTime DayOff { get; set; }
        public bool Recursive { get; set; }
        public int Interval { get; set; }
        public int StudentId { get; set; }
    }

    public class CreateUnavailableDayCommandHandler : IRequestHandler<CreateUnavailableDayCommand, int>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateUnavailableDayCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task<int> Handle(CreateUnavailableDayCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}