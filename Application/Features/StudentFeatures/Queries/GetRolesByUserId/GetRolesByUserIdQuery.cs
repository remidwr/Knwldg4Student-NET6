using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.StudentFeatures.Queries.GetRolesByUserId
{
    public record GetRolesByUserIdQuery(string id) : IRequest<List<UsersRoleDto>>;

    public class GetRolesByUserIdHandler : IRequestHandler<GetRolesByUserIdQuery, List<UsersRoleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAuth0Api _auth0Api;

        public GetRolesByUserIdHandler(IMapper mapper,
            IAuth0Api auth0Api)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _auth0Api = auth0Api ?? throw new ArgumentNullException(nameof(auth0Api));
        }

        public async Task<List<UsersRoleDto>> Handle(GetRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var usersRoles = await _auth0Api.GetUsersRoleAsync(request.id);

            if (!usersRoles.Any())
                return new List<UsersRoleDto>();

            var usersRoleDtos = _mapper.Map<IEnumerable<UsersRoleDto>>(usersRoles);

            return usersRoleDtos.ToList();
        }
    }
}