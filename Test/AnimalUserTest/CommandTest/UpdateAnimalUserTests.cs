using Application.Commands.AnimalUser.UpdateAnimalUser;
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
    public class UpdateAnimalUserByUserIdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnTrue_WhenUpdateUserAnimalAsyncSucceeds()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var mockLogger = new Mock<ILogger<UpdateAnimalUserByUserIdCommandHandler>>();

            var handler = new UpdateAnimalUserByUserIdCommandHandler(mockRepository.Object, mockLogger.Object);

            var command = new UpdateAnimalUserByUserIdCommand(Guid.NewGuid(), Guid.NewGuid());

            // Mock the repository method to indicate success on updating
            mockRepository.Setup(repo => repo.AddUserAnimalAsync(It.IsAny<AnimalUserModel>()))
                          .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            // Add more specific assertions based on your application logic
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenUpdateUserAnimalAsyncFails()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var mockLogger = new Mock<ILogger<UpdateAnimalUserByUserIdCommandHandler>>();

            var handler = new UpdateAnimalUserByUserIdCommandHandler(mockRepository.Object, mockLogger.Object);

            var command = new UpdateAnimalUserByUserIdCommand(Guid.NewGuid(), Guid.NewGuid());

            // Mock the repository method to indicate failure on updating
            mockRepository.Setup(repo => repo.AddUserAnimalAsync(It.IsAny<AnimalUserModel>()))
                          .ThrowsAsync(new Exception("Simulating an error during update"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));
            // You can add more specific assertions based on your error handling logic
        }
    }
}