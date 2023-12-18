using Application.Commands.Birds.DeleteBird;
using Domain.Models.Animal;
using Infrastructure.DataDbContex;
using Moq;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class DeleteBirdByIdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidBirdId_DeletesDog()
        {
            // Arrange
            var deletedBirdId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedBirdId))
                .ReturnsAsync(new AnimalModel { AnimalId = deletedBirdId }); // Dog exists in the repository

            var handler = new DeleteBirdByIdCommandHandler(mockRepository.Object);
            var command = new DeleteBirdByIdCommand(deletedBirdId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Expected deletion to succeed");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedBirdId), Times.Once, "Expected DeleteAsync to be called once");
        }

        [Test]
        public async Task Handle_InvalidBirdId_ReturnsFalse()
        {
            // Arrange
            var deletedBirdId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedBirdId))
                .ReturnsAsync((AnimalModel)null); // Dog does not exist in the repository

            var handler = new DeleteBirdByIdCommandHandler(mockRepository.Object);
            var command = new DeleteBirdByIdCommand(deletedBirdId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedBirdId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}
