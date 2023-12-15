using Application.Queries.Users;
using Domain.Models;
using Infrastructure.DataDbContex;
using Moq;
using Application.Queries.Users.GetAll;

namespace Test.UserTests.QueryTest
{
    [TestFixture]
    public class GetAllUsersTests
    {
        private GetAllUsersQueryHandler _handler;
        private Mock<UserInterface> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            // Använd Moq för att skapa en generisk mock av IAnimalsRepository
            _mockRepository = new Mock<UserInterface>();
            _handler = new GetAllUsersQueryHandler(_mockRepository.Object);
        }

        [Test]
        public async Task ShouldReturnAllUsers()
        {
            // Arrange
            var query = new GetAllUsersQuery();
            var expectedUser = new List<UserS>
            {
                new UserS { Id = Guid.NewGuid(), UserName = "Dog1" },
                new UserS { Id = Guid.NewGuid(), UserName = "Dog2" },
            };

            _mockRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(expectedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<UserS>>());
            Assert.That(result.Count, Is.EqualTo(expectedUser.Count));

            // Du kan även göra specifika kontroller för hundarna om det behövs
            // Exempel:
            Assert.That(result[0].Id, Is.EqualTo(expectedUser[0].Id));
            Assert.That(result[0].UserName, Is.EqualTo(expectedUser[0].UserName));
            // Fortsätt för resten av attributen om det behövs
        }
    }
}