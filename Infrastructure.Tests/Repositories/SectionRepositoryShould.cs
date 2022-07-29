using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories
{
    public class SectionRepositoryShould
    {
        private readonly Mock<KnwldgContext> _mockContext;
        private readonly Mock<DbSet<Section>> _mockSet;
        private readonly CancellationToken _cancellationToken;

        public SectionRepositoryShould()
        {
            _mockContext = new Mock<KnwldgContext>();
            _mockSet = new Mock<DbSet<Section>>();
            _cancellationToken = new CancellationToken(false);
        }

        [Fact]
        public async void GetAllSectionsAsync_ReturnsAllSectionsOrderedByTitle()
        {
            // Arrange
            var fixture = new Fixture();
            var sections = fixture.CreateMany<Section>().AsQueryable();

            _mockSet.As<IAsyncEnumerable<Section>>()
                .Setup(x => x.GetAsyncEnumerator(_cancellationToken))
                .Returns(new TestAsyncEnumerator<Section>(sections.GetEnumerator()));

            _mockSet.As<IQueryable<Section>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<Section>(sections.Provider));

            _mockSet.As<IQueryable<Section>>().Setup(m => m.Expression).Returns(sections.Expression);
            _mockSet.As<IQueryable<Section>>().Setup(m => m.ElementType).Returns(sections.ElementType);
            _mockSet.As<IQueryable<Section>>().Setup(m => m.GetEnumerator()).Returns(() => sections.GetEnumerator());

            _mockContext.Setup(x => x.Sections).Returns(_mockSet.Object);

            // Act
            var sut = new SectionRepository(_mockContext.Object);
            var result = await sut.GetAllAsync(_cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(sections);
            result.Should().BeInAscendingOrder(x => x.Title);
        }
    }
}