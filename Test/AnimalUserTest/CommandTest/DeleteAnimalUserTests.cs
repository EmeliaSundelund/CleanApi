using Application.Commands.AnimalUser.DeleteAnimalUser;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.AnimalUserTest.CommandTest
{
    [TestFixture]
    public class DeleteAnimalUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnTrue_WhenDeleteUserAnimalAsyncSucceeds()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var mockLogger = new Mock<ILogger<DeleteAnimalUserCommandHandler>>();

            var handler = new DeleteAnimalUserCommandHandler(mockRepository.Object, mockLogger.Object);

            var command = new DeleteAnimalUserCommand(Guid.NewGuid());

            // Mock the repository method to return an existing animal user
            mockRepository.Setup(repo => repo.GetByKeyAsync(It.IsAny<Guid>()))
                          .ReturnsAsync(new AnimalUserModel());

            // Mock the repository method to indicate success on deletion
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                          .Returns(Task.FromResult(true));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            // Add more specific assertions based on your application logic
        }

        [Test]
        public async Task Handle_ShouldReturnFalse_WhenDeleteUserAnimalAsyncFails()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var mockLogger = new Mock<ILogger<DeleteAnimalUserCommandHandler>>();

            var handler = new DeleteAnimalUserCommandHandler(mockRepository.Object, mockLogger.Object);

            var command = new DeleteAnimalUserCommand(Guid.NewGuid());

            // Mock the repository method to return null, indicating that the animal user does not exist
            mockRepository.Setup(repo => repo.GetByKeyAsync(It.IsAny<Guid>()))
                          .ReturnsAsync((AnimalUserModel)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
            // Add more specific assertions based on your application logic
        }
    }
}