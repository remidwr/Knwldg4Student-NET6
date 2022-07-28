using Application.Features.SectionFeatures.Queries.GetCoursesBySectionId;
using Application.Features.SectionFeatures.Queries.GetSectionById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Features.SectionFeatures.Queries.GetCoursesBySectionId
{
    public class GetCoursesBySectionIdQueryShould
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISectionRepository> _mockSectionRepository;
        private readonly GetCoursesBySectionIdQueryHandler _getCoursesBySectionIdQueryHandler;
        private readonly CancellationToken _cancellationToken;
        private readonly int _sectionId;

        public GetCoursesBySectionIdQueryShould()
        {
            _sectionId = 1;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
               {
                    typeof(Section),
                    typeof(SectionDto),
               });
            });

            _mockMapper = new Mock<IMapper>();
            var sectionDtos = new List<CourseDto> { new CourseDto() };
            _mockMapper.Setup(x => x.Map<IEnumerable<CourseDto>>(It.IsAny<object>())).Returns(sectionDtos);
            _mockSectionRepository = new Mock<ISectionRepository>();
            _getCoursesBySectionIdQueryHandler = new GetCoursesBySectionIdQueryHandler(_mockMapper.Object, _mockSectionRepository.Object);
            _cancellationToken = new CancellationToken(false);
        }

        [Fact]
        public async void ReturnCoursesWhenSectionExists()
        {
            // Arrange
            var fixture = new Fixture();
            _mockSectionRepository.Setup(x => x.GetCoursesBySectionIdAsync(_sectionId, _cancellationToken)).ReturnsAsync(fixture.CreateMany<Course>());

            // Act
            var courses = await _getCoursesBySectionIdQueryHandler.Handle(new GetCoursesBySectionIdQuery(_sectionId), _cancellationToken);

            // Assert
            _mockSectionRepository.Verify(x => x.GetCoursesBySectionIdAsync(_sectionId, _cancellationToken), Times.Once);
            courses.Should().NotBeNullOrEmpty();
        }
    }
}