using Application.Commands.Cats.DeleteCat;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class DeleteCatByIdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCatId_DeletesCat()
        {
            // Arrange
            var deletedCatId = Guid.NewGuid();
            var mockRepository = new Mock<IAnimalsRepository>();
            var mockLogger = new Mock<ILogger<DeleteCatByIdCommandHandler>>();

            mockRepository.Setup(repo => repo.GetByIdAsync(deletedCatId))
                .ReturnsAsync(new AnimalModel { AnimalId = deletedCatId });

            var handler = new DeleteCatByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteCatByIdCommand(deletedCatId);

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
            var deletedCatId = Guid.NewGuid();
            var mockRepository = new Mock<IAnimalsRepository>();
            var mockLogger = new Mock<ILogger<DeleteCatByIdCommandHandler>>();

            mockRepository.Setup(repo => repo.GetByIdAsync(deletedCatId))
                .ReturnsAsync((AnimalModel)null);

            var handler = new DeleteCatByIdCommandHandler(mockRepository.Object, mockLogger.Object);
            var command = new DeleteCatByIdCommand(deletedCatId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedCatId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}