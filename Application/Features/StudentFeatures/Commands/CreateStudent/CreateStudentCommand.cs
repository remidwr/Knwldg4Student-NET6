using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.StudentFeatures.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuth0Api _auth0Api;

        public CreateStudentCommandHandler(IStudentRepository studentRepository,
            IAuth0Api auth0Api)
        {
            _studentRepository = studentRepository;
            _auth0Api = auth0Api;
        }

        public async Task<int> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var user = await _auth0Api.CreateUserAsync(command);

            if (user == null)
            {
                //TODO throw exception
                return 0;
            }

            var student = new Student(user.UserId, command.Username, command.Email, command.Password);
            _studentRepository.Add(student);

            await _studentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}