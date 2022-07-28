namespace Application.Tests.Features.SectionFeatures.Queries.GetSections
{
    public class GetSectionsQueryShould
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISectionRepository> _mockSectionRepository;
        private readonly GetSectionsQueryHandler _getSectionsQueryHandler;
        private readonly CancellationToken _cancellationToken;

        public GetSectionsQueryShould()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
               {
                    typeof(Section),
                    typeof(SectionDto),
               });
            });

            _mockMapper = new Mock<IMapper>();
            var sectionDtos = new List<SectionDto> { new SectionDto() };
            _mockMapper.Setup(x => x.Map<IList<SectionDto>>(It.IsAny<object>())).Returns(sectionDtos);

            _mockSectionRepository = new Mock<ISectionRepository>();
            _getSectionsQueryHandler = new GetSectionsQueryHandler(_mockMapper.Object, _mockSectionRepository.Object);
            _cancellationToken = new CancellationToken(false);
        }

        [Fact]
        public async void GetSections_ReturnsAllSections()
        {
            // Arrange
            var fixture = new Fixture();
            _mockSectionRepository.Setup(x => x.GetAllAsync(_cancellationToken)).ReturnsAsync(fixture.CreateMany<Section>());

            // Act
            var sections = await _getSectionsQueryHandler.Handle(fixture.Create<GetSectionsQuery>(), _cancellationToken);

            // Assert
            _mockSectionRepository.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            sections.Should().NotBeNull();
            sections.Sections.Should().HaveCountGreaterThan(0);
        }
    }
}