using Application.Commands.Dogs.DeleteDog;
using Application.Commands.User.DeleteUser;
using Domain.Models.Animal;
using Infrastructure.DataDbContex;
using Moq;


namespace Application.Tests.Commands.Dogs
{
    [TestFixture]
    public class DeleteDogByIdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidDogId_DeletesDog()
        {
            // Arrange
            var deletedDogId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedDogId))
                .ReturnsAsync(new AnimalModel { id = deletedDogId }); // Dog exists in the repository

            var handler = new DeleteDogByIdCommandHandler(mockRepository.Object);
            var command = new DeleteDogByIdCommand(deletedDogId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Expected deletion to succeed");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedDogId), Times.Once, "Expected DeleteAsync to be called once");
        }

        [Test]
        public async Task Handle_InvalidDogId_ReturnsFalse()
        {
            // Arrange
            var deletedDogId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedDogId))
                .ReturnsAsync((AnimalModel)null); // Dog does not exist in the repository

            var handler = new DeleteDogByIdCommandHandler(mockRepository.Object);
            var command = new DeleteDogByIdCommand(deletedDogId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedDogId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}
