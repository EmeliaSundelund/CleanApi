using Application.AnimalUsers.Commands.AddAnimalUser;
using Application.Commands.AnimalUser.AddAnimalUser;
using Application.Dtos;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using Moq;

namespace Test.AnimalUserTest.CommandTest
{
    [TestFixture]
    public class AddAnimalUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnTrue_WhenAddUserAnimalAsyncSucceeds()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var handler = new AddAnimalUserCommandHandler(mockRepository.Object);

            var command = new AddAnimalUserCommand(new AnimalUserDto
            {
                UserId = Guid.NewGuid(),
                AnimalId = Guid.NewGuid(),
            });

            // Mock the repository method to indicate success
            mockRepository.Setup(repo => repo.AddUserAnimalAsync(It.IsAny<AnimalUserModel>()))
                          .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            // Add more specific assertions based on your application logic
        }

        [Test]
        public async Task Handle_ShouldReturnFalse_WhenAddUserAnimalAsyncFails()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var handler = new AddAnimalUserCommandHandler(mockRepository.Object);

            var command = new AddAnimalUserCommand(new AnimalUserDto
            {
                UserId = Guid.NewGuid(),
                AnimalId = Guid.NewGuid(),
            });

            // Mock the repository method to indicate failure
            mockRepository.Setup(repo => repo.AddUserAnimalAsync(It.IsAny<AnimalUserModel>()))
                          .ReturnsAsync(false);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
            // Add more specific assertions based on your application logic
        }
    }
}