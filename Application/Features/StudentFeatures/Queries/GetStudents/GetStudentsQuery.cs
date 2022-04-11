namespace Application.Features.StudentFeatures.Queries.GetStudents
{
    public class GetStudentsQuery : IRequest<StudentsVm>
    {
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, StudentsVm>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public GetStudentsQueryHandler(IMapper mapper,
            IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<StudentsVm> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return new StudentsVm(studentDtos);
        }
    }
}