using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.AggregatesModel.SectionAggregate
{
    public class SectionShould
    {
        [Fact]
        public void CallConstructor_InitializeSectionProperties()
        {
            // Act
            var sut = (Section)Activator.CreateInstance(typeof(Section), true);

            // Assert
            sut.Title.Should().BeEquivalentTo(string.Empty);
            sut.Courses.Should().NotBeNull();
            sut.Courses.Should().BeEmpty();
        }

        [Fact]
        public void CallConstructor_ToInitializeSectionTitle()
        {
            // Arrange
            var fixture = new Fixture();
            var title = fixture.Create<string>();

            // Act
            var sut = new Section(title);

            // Assert
            sut.Title.Should().BeEquivalentTo(title);
        }

        [Fact]
        public void CallConstructorWithEmptyTitle_ThrowsAnArgumentNullException()
        {
            // Arrange
            var title = string.Empty;

            // Act
            var invocation = () => new Section(title);

            // Assert
            invocation.Should().ThrowExactly<ArgumentNullException>()
                               .WithParameterName(nameof(title));
        }

        [Fact]
        public void CallConstructor_ToInitializeSectionTitleAndCourses()
        {
            // Arrange
            var fixture = new Fixture();
            var title = fixture.Create<string>();
            var courses = fixture.CreateMany<Course>().ToList();

            // Act
            var sut = new Section(title, courses);

            // Assert
            sut.Title.Should().BeEquivalentTo(title);
            sut.Courses.Should().BeEquivalentTo(courses);
        }

        [Fact]
        public void OrderCoursesByLabel()
        {
            // Arrange
            var fixture = new Fixture();
            var courses = fixture.CreateMany<Course>().ToList();
            var section = new Section(fixture.Create<string>(), courses);

            // Act
            section.OrderCoursesByLabel();

            // Assert
            section.Courses.Should().BeInAscendingOrder(x => x.Label);
        }

        [Fact]
        public void NotOrderCoursesByLabel_WhenCoursesListIsNullOrEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            var section = new Section(fixture.Create<string>(), null);

            // Act
            section.OrderCoursesByLabel();

            // Assert
            section.Courses.Should().BeNullOrEmpty();
        }
    }
}