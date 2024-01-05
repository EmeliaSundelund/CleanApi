using Application.Commands.User.DeleteUser.DeleteUserByIdCommandHandler;
using Application.Commands.Users.DeleteUser;
using Domain.Models.Person;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.UserTests.CommandTest
{
    [TestFixture]
    public class DeleteUserTests
    {
        [Test]
        public async Task Handle_ValidUserId_DeletesUser()
        {
            // Arrange
            var deletedUserId = Guid.NewGuid();
            var mockRepository = new Mock<IUserInterface>();
            var loggerMock = new Mock<ILogger<DeleteUserByIdCommandHandler>>(); // Add logger mock
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedUserId))
                .ReturnsAsync(new UserModel { UserId = deletedUserId });

            var handler = new DeleteUserByIdCommandHandler(mockRepository.Object, loggerMock.Object); // Pass logger mock
            var command = new DeleteUserByIdCommand(deletedUserId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Expected deletion to succeed");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedUserId), Times.Once, "Expected DeleteAsync to be called once");
        }

        [Test]
        public async Task Handle_InvalidUserId_ReturnsFalse()
        {
            // Arrange
            var deletedUserId = Guid.NewGuid();
            var mockRepository = new Mock<IUserInterface>();
            var loggerMock = new Mock<ILogger<DeleteUserByIdCommandHandler>>(); // Add logger mock
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedUserId))
                .ReturnsAsync((UserModel)null);

            var handler = new DeleteUserByIdCommandHandler(mockRepository.Object, loggerMock.Object); // Pass logger mock
            var command = new DeleteUserByIdCommand(deletedUserId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedUserId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}