using Application.Common.ExternalApi.Auth0Api;
using Application.Features.StudentFeatures.Commands.AssignRolesToStudent;

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
            await AssignRoleToUserAsync(user);

            var student = new Student(user.UserId, command.Username, command.Email);
            _studentRepository.Add(student);

            await _studentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return student.Id;
        }

        private async Task AssignRoleToUserAsync(UserCreationResponse user)
        {
            var roleIds = new List<string> { "rol_2E6CcGbKlcC2nEJ4" };
            var assignRolesToStudentCommand = new AssignRolesToStudentCommand(user.UserId, roleIds);

            await _auth0Api.AssignRolesToUserAsync(assignRolesToStudentCommand);
        }
    }
}