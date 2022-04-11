namespace Application.Features.StudentFeatures.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<StudentDetailedDto>
    {
        public int Id { get; set; }
    }

    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailedDto>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdHandler(IMapper mapper,
            IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<StudentDetailedDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            return _mapper.Map<StudentDetailedDto>(student);
        }
    }
}