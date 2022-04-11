namespace Application.Features.SectionFeatures.Queries.GetSectionById
{
    public class GetSectionByIdQuery : IRequest<SectionDetailedDto>
    {
        public int Id { get; set; }
    }

    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionDetailedDto>
    {
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public GetSectionByIdQueryHandler(IMapper mapper,
            ISectionRepository sectionRepository)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<SectionDetailedDto> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetSectionByIdAsync(request.Id, cancellationToken);

            if (section == null)
            {
                throw new NotFoundException(nameof(Section), request.Id);
            }

            return _mapper.Map<SectionDetailedDto>(section);
        }
    }
}