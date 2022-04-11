namespace Application.Features.SectionFeatures.Queries.GetSections
{
    public class GetSectionsQuery : IRequest<SectionsVm>
    {
    }

    public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, SectionsVm>
    {
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public GetSectionsQueryHandler(IMapper mapper,
            ISectionRepository sectionRepository)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<SectionsVm> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
        {
            var sections = await _sectionRepository.GetAllAsync(cancellationToken);
            var sectionDtos = _mapper.Map<IList<SectionDto>>(sections);

            return new SectionsVm(sectionDtos);
        }
    }
}