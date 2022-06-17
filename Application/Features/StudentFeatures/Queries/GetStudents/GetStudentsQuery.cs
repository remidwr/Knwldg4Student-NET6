using Application.Common.ExternalApi.Auth0Api;
using Application.Common.Identity;

namespace Application.Features.StudentFeatures.Queries.GetStudents
{
    public class GetStudentsQuery : IRequest<StudentsVm>
    {
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, StudentsVm>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IIdentityService _identityService;

        public GetStudentsQueryHandler(IMapper mapper,
            IStudentRepository studentRepository,
            IIdentityService identityService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<StudentsVm> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _identityService.GetUserId();

            var students = await _studentRepository.GetAllAsync();
            students = students.Where(s => s.ExternalId != currentUserId);

            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return new StudentsVm(studentDtos);
        }
    }
}