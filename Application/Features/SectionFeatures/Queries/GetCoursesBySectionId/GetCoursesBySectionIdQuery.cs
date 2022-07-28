namespace Application.Features.SectionFeatures.Queries.GetCoursesBySectionId
{
    public record GetCoursesBySectionIdQuery(int Id) : IRequest<IEnumerable<CourseDto>>;

    public class GetCoursesBySectionIdQueryHandler : IRequestHandler<GetCoursesBySectionIdQuery, IEnumerable<CourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public GetCoursesBySectionIdQueryHandler(IMapper mapper,
            ISectionRepository sectionRepository)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetCoursesBySectionIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _sectionRepository.GetCoursesBySectionIdAsync(request.Id, cancellationToken);

            if (courses == null)
                throw new NotFoundException(nameof(Section), request.Id);

            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return courseDtos;
        }
    }
}