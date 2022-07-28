using Application.Common.Exceptions;

namespace Application.Tests.Common.Exceptions
{
    public class NotFoundExceptionShould
    {
        [Fact]
        public void ReturnASpecificMessageWhenTheExceptionIsThrown()
        {
            // Arrange
            var fixture = new Fixture();
            var name = fixture.Create<string>();
            var key = fixture.Create<int>();

            // Act
            var invocation = () => { throw new NotFoundException(name, key); };

            // Assert
            invocation.Should().ThrowExactly<NotFoundException>()
                               .WithMessage($"Entity \"{name}\" ({key}) was not found.");
        }
    }
}