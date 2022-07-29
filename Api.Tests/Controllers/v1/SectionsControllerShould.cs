using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Tests.Controllers.v1
{
    public class SectionsControllerShould
    {
        private readonly Mock<IIdentityService> _mockIdentity;

        public SectionsControllerShould()
        {
            _mockIdentity = new Mock<IIdentityService>();
        }

        [Fact]
        public async void ReturnStatusCode200OK()
        {
            // Arrange
            var mockMediatR = new Mock<IMediator>();
            var query = new GetSectionsQuery();
            mockMediatR.Setup(x => x.Send(query, CancellationToken.None));
            var controller = new SectionsController(_mockIdentity.Object);
            // TODO : Mediator is null

            // Act
            var result = await controller.Get();

            // Assert
            mockMediatR.Verify(x => x.Send(query, CancellationToken.None), Times.Once);
        }
    }
}