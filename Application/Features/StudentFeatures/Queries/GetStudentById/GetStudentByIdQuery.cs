namespace Application.Features.StudentFeatures.Queries.GetStudentById
{
    public record GetStudentByIdQuery(int Id) : IRequest<StudentDetailedDto>;

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

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            return _mapper.Map<StudentDetailedDto>(student);
        }
    }
}