using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ExternalApi.Auth0Api;
using Application.Features.StudentFeatures.Commands.CreateStudent;
using Domain.AggregatesModel.StudentAggregate;

namespace Application.Tests.Features.StudentFeatures.Commands.CreateStudent
{
    public class CreateStudentCommandShould
    {
        private readonly Mock<IStudentRepository> _mockRepository;
        private readonly Mock<IAuth0Api> _mockAuth0Api;
        private readonly CancellationToken _cancellationToken;

        public CreateStudentCommandShould()
        {
            _mockRepository = new Mock<IStudentRepository>();
            _mockAuth0Api = new Mock<IAuth0Api>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async void Handle_ToCreateANewStudent()
        {
            // Arrange
            var fixture = new Fixture();
            var command = fixture.Freeze<CreateStudentCommand>();
            var user = fixture.Build<UserCreationResponse>()
                                              .With(x => x.Nickname, command.Username)
                                              .With(x => x.Email, command.Email)
                                              .Create();

            _mockAuth0Api.Setup(x => x.CreateUserAsync(command)).ReturnsAsync(user);
            var student = new Student(user.UserId, command.Username, command.Email);
            _mockRepository.Setup(x => x.Add(student));
            _mockRepository.Setup(x => x.UnitOfWork.SaveChangesAsync(_cancellationToken));
            var commandHandler = new CreateStudentCommandHandler(_mockRepository.Object, _mockAuth0Api.Object);

            // Act
            var result = await commandHandler.Handle(command, _cancellationToken);

            // Assert
            _mockAuth0Api.Verify(x => x.CreateUserAsync(It.IsAny<CreateStudentCommand>()), Times.Once);
            _mockRepository.Verify(x => x.Add(It.IsAny<Student>()), Times.Once);
            _mockRepository.Verify(x => x.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}