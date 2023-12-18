using Application.Commands.User.DeleteUser.DeleteUserByIdCommandHandler;
using Application.Commands.Users.DeleteUser;
using Domain.Models.Animal;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using Moq;


namespace Tests.UserTests.CommandTest
{
    [TestFixture]
    public class DeleteUserTests
    {
        [Test]
        public async Task Handle_ValidUserId_DeletesUser()
        {
            // Arrange
            var deletedUserId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<UserInterface>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedUserId))
                .ReturnsAsync(new UserModel { UserId = deletedUserId }); // Dog exists in the repository

            var handler = new DeleteUserByIdCommandHandler(mockRepository.Object);
            var command = new DeleteUserByIdCommand(deletedUserId); // Pass the Guid to the constructor

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
            var deletedUserId = Guid.NewGuid(); // Use Guid for DeletedDogId
            var mockRepository = new Mock<UserInterface>();
            mockRepository.Setup(repo => repo.GetByIdAsync(deletedUserId))
                .ReturnsAsync((UserModel)null); // Dog does not exist in the repository

            var handler = new DeleteUserByIdCommandHandler(mockRepository.Object);
            var command = new DeleteUserByIdCommand(deletedUserId); // Pass the Guid to the constructor

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False, "Expected deletion to fail");
            mockRepository.Verify(repo => repo.DeleteAsync(deletedUserId), Times.Never, "Expected DeleteAsync not to be called");
        }
    }
}
