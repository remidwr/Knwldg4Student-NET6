using Application.Common.Exceptions;
using Application.Features.SectionFeatures.Queries.GetSectionById;

namespace Application.Tests.Features.SectionFeatures.Queries.GetSectionById
{
    public class SectionFeaturesTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISectionRepository> _mockSectionRepository;
        private readonly GetSectionByIdQueryHandler _getSectionByIdQueryHandler;
        private readonly Mock<Section> _mockSection;
        private readonly int _sectionId;
        private readonly CancellationToken _cancellationToken;

        public SectionFeaturesTests()
        {
            _sectionId = 1;
            var sectionDetailedDto = new SectionDetailedDto { Id = _sectionId };

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
               {
                    typeof(Section),
                    typeof(SectionDto),
               });
            });

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(x => x.Map<SectionDetailedDto>(It.IsAny<object>())).Returns(sectionDetailedDto);
            _mockSectionRepository = new Mock<ISectionRepository>();

            _getSectionByIdQueryHandler = new GetSectionByIdQueryHandler(_mockMapper.Object, _mockSectionRepository.Object);
            _mockSection = new Mock<Section>();
            _cancellationToken = new CancellationToken(false);
        }

        [Fact]
        public async void Handle_ReturnsOneSection()
        {
            // Arrange
            var sectionDetailedDto = new SectionDetailedDto { Id = _sectionId };
            _mockMapper.Setup(x => x.Map<SectionDetailedDto>(It.IsAny<object>())).Returns(sectionDetailedDto);
            _mockSection.Setup(x => x.Id).Returns(_sectionId);
            _mockSectionRepository.Setup(x => x.GetSectionByIdAsync(_sectionId, _cancellationToken)).ReturnsAsync(_mockSection.Object);

            // Act
            var section = await _getSectionByIdQueryHandler.Handle(new GetSectionByIdQuery(_sectionId), _cancellationToken);

            // Assert
            _mockSectionRepository.Verify(x => x.GetSectionByIdAsync(_sectionId, It.IsAny<CancellationToken>()), Times.Once);
            section.Should().NotBeNull();
            section.Id.Should().Be(_sectionId);
        }

        [Fact]
        public async void Handle_ReturnsArgumentNullExceptionWhenSectionIsNotFound()
        {
            // Arrange
            _mockSectionRepository.Setup(x => x.GetSectionByIdAsync(_sectionId, _cancellationToken)).Returns(Task.FromResult<Section>(null));
            var getSectionByIdQueryHandler = new GetSectionByIdQueryHandler(_mockMapper.Object, _mockSectionRepository.Object);
            var getSectionByIdQuery = new GetSectionByIdQuery(_sectionId);

            // Act
            var getSectionById = async () => await getSectionByIdQueryHandler.Handle(getSectionByIdQuery, _cancellationToken);

            // Assert
            await getSectionById.Should().ThrowAsync<NotFoundException>();
            _mockSectionRepository.Verify(x => x.GetSectionByIdAsync(_sectionId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}