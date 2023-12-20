using Application.Commands.Cats.DeleteCat;
using Domain.Models.Animal;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using Moq;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class DeleteCatByIdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCatId_DeletesCat()
        {
            // Arrange
            var deletedCatId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedCatId))
                .ReturnsAsync(new AnimalModel { AnimalId = deletedCatId }); // Dog exists in the repository

            var handler = new DeleteCatByIdCommandHandler(mockRepository.Object);
            var command = new DeleteCatByIdCommand(deletedCatId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Expected deletion to succeed");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedCatId), Times.Once, "Expected DeleteAsync to be called once");
        }

        [Test]
        public async Task Handle_InvalidCatId_ReturnsFalse()
        {
            // Arrange
            var deletedCatId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<IAnimalsRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedCatId))
                .ReturnsAsync((AnimalModel)null); // Dog does not exist in the repository

            var handler = new DeleteCatByIdCommandHandler(mockRepository.Object);
            var command = new DeleteCatByIdCommand(deletedCatId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedCatId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}