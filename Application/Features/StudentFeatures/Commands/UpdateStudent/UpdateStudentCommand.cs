namespace Application.Features.StudentFeatures.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Unit> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(command.Id);

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), command.Id);
            }

            student.UpdatePersonalInformations(command.FirstName, command.LastName, command.Description);

            await _studentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}