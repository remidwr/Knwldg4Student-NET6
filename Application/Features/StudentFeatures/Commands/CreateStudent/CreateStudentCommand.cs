namespace Application.Features.StudentFeatures.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<int> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = new Student(command.FirstName, command.LastName, command.Email, command.Description);
            _studentRepository.Add(student);

            await _studentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}