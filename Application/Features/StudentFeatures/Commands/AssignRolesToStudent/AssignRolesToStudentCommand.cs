using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.StudentFeatures.Commands.AssignRolesToStudent
{
    public class AssignRolesToStudentCommand : IRequest
    {
        public AssignRolesToStudentCommand(string studentExternalId, IEnumerable<string> roleIds)
        {
            StudentExternalId = studentExternalId;
            RoleIds = roleIds;
        }

        public string StudentExternalId { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }

    public class AssignRolesToStudentCommandHandler : IRequestHandler<AssignRolesToStudentCommand>
    {
        private readonly IAuth0Api _auth0Api;

        public AssignRolesToStudentCommandHandler(IAuth0Api auth0Api)
        {
            _auth0Api = auth0Api;
        }

        public async Task<Unit> Handle(AssignRolesToStudentCommand command, CancellationToken cancellationToken)
        {
            await _auth0Api.AssignRolesToUserAsync(command);

            return Unit.Value;
        }
    }
}