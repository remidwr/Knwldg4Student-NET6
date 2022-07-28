using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.AggregatesModel.SectionAggregate
{
    public class CourseShould
    {
        [Fact]
        public void CallConstructor_InitializeCourseProperties()
        {
            // Act
            var sut = (Course)Activator.CreateInstance(typeof(Course), true);

            // Assert
            sut.Students.Should().NotBeNull();
            sut.Students.Should().BeEmpty();
        }

        [Fact]
        public void CallConstructor_ToInitializeCourseSectionIdAndLabel()
        {
            // Arrange
            var fixture = new Fixture();
            var label = fixture.Create<string>();
            var sectionId = fixture.Create<int>();

            // Act
            var sut = new Course(sectionId, label);

            // Assert
            sut.SectionId.Should().Be(sectionId);
            sut.Label.Should().BeEquivalentTo(label);
        }

        [Fact]
        public void CallConstructorWithEmptyLabel_ThrowsAnArgumentNullException()
        {
            // Arrange
            var fixture = new Fixture();
            var sectionId = fixture.Create<int>();
            var label = string.Empty;

            // Act
            var sut = () => new Course(sectionId, label);

            // Assert
            sut.Should().ThrowExactly<ArgumentNullException>().WithParameterName(nameof(label));
        }
    }
}