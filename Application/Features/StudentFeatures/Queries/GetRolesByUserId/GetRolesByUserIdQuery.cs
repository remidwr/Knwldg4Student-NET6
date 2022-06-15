using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.StudentFeatures.Queries.GetRolesByUserId
{
    public record GetRoleByUserIdQuery(string Id) : IRequest<UsersRoleDto>;

    public class GetRoleByUserIdHandler : IRequestHandler<GetRoleByUserIdQuery, UsersRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuth0Api _auth0Api;

        public GetRoleByUserIdHandler(IMapper mapper,
            IAuth0Api auth0Api)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _auth0Api = auth0Api ?? throw new ArgumentNullException(nameof(auth0Api));
        }

        public async Task<UsersRoleDto> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
        {
            var usersRoles = await _auth0Api.GetUsersRolesAsync(request.Id);

            if (!usersRoles.Any())
                return null;

            var usersRoleDto = _mapper.Map<UsersRoleDto>(usersRoles.FirstOrDefault());

            return usersRoleDto;
        }
    }
}