using Application.Commands.Dogs.DeleteDog;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var mockLogger = new Mock<ILogger<DeleteDogByIdCommandHandler>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedDogId))
                .ReturnsAsync(new AnimalModel { AnimalId = deletedDogId }); // Dog exists in the repository

            var handler = new DeleteDogByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteDogByIdCommand(deletedDogId);

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
            var mockLogger = new Mock<ILogger<DeleteDogByIdCommandHandler>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedDogId))
                .ReturnsAsync((AnimalModel)null); // Dog does not exist in the repository

            var handler = new DeleteDogByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteDogByIdCommand(deletedDogId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedDogId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}