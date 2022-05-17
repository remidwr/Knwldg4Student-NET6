using Application.Common.ExternalApi.Auth0Api;

namespace Application.Features.RoleFeatures.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<RolesVm>
    {
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RolesVm>
    {
        private readonly IMapper _mapper;
        private readonly IAuth0Api _auth0Api;

        public GetRolesQueryHandler(IAuth0Api auth0Api,
            IMapper mapper)
        {
            _auth0Api = auth0Api;
            _mapper = mapper;
        }

        public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _auth0Api.GetRolesAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return new RolesVm(roleDtos);
        }
    }
}