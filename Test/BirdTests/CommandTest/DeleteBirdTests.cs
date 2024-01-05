using Application.Commands.Bird.DeleteBird;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class DeleteBirdTests
    {
        [Test]
        public async Task Handle_ValidBirdId_DeletesBird()
        {
            // Arrange
            var deletedBirdId = Guid.NewGuid();
            var mockRepository = new Mock<IAnimalsRepository>();
            var mockLogger = new Mock<ILogger<DeleteBirdByIdCommandHandler>>();

            mockRepository.Setup(repo => repo.GetByIdAsync(deletedBirdId))
                .ReturnsAsync(new AnimalModel { AnimalId = deletedBirdId });

            var handler = new DeleteBirdByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteBirdByIdCommand(deletedBirdId);

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
            var deletedBirdId = Guid.NewGuid();
            var mockRepository = new Mock<IAnimalsRepository>();
            var mockLogger = new Mock<ILogger<DeleteBirdByIdCommandHandler>>();

            mockRepository.Setup(repo => repo.GetByIdAsync(deletedBirdId))
                .ReturnsAsync((AnimalModel)null);

            var handler = new DeleteBirdByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteBirdByIdCommand(deletedBirdId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedBirdId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}