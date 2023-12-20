using Moq;
using Application.Queries.Users.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;
using Domain.Models.Person;
using Infrastructure.DataDbContex.Interfaces;

namespace Application.Test.UserTests.QueryTest
{
    [TestFixture]
    public class GetUserByIdTests
    {
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectUser()
        {
            // Arrange
            var mockRepository = new Mock<IUserInterface>();
            var expectedUser = new UserModel { UserId = Guid.NewGuid(), UserName = "Buddy" };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedUser);

            var queryHandler = new GetUserByIdQueryHandler(mockRepository.Object);
            var query = new GetUserByIdQuery(expectedUser.UserId);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedUser));
        }
    }
}