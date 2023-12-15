using Moq;
using Application.Queries.Users.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;

namespace Application.Test.UserTests.QueryTest
{
    [TestFixture]
    public class GetUserByIdTests
    {
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectUser()
        {
            // Arrange
            var mockRepository = new Mock<UserInterface>();
            var expectedUser = new UserS { Id = Guid.NewGuid(), UserName = "Buddy" };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedUser);

            var queryHandler = new GetUserByIdQueryHandler(mockRepository.Object);
            var query = new GetUserByIdQuery(expectedUser.Id);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedUser));
        }
    }
}