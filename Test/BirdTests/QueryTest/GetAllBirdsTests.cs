using Application.Queries.Birds;
using Domain.Models;
using Infrastructure.DataDbContex;
using Moq;
using Application.Queries.Birds.GetAll;
using Infrastructure.DataDbContex.Interfaces;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private GetAllBirdsQueryHandler _handler;
        private Mock<IAnimalsRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            // Använd Moq för att skapa en generisk mock av IAnimalsRepository
            _mockRepository = new Mock<IAnimalsRepository>();
            _handler = new GetAllBirdsQueryHandler(_mockRepository.Object);
        }

        [Test]
        public async Task ShouldReturnAllBirds()
        {
            // Arrange
            var query = new GetAllBirdsQuery();
            var expectedBirds = new List<Bird>
            {
                new Bird { AnimalId = Guid.NewGuid(), Name = "Bird1" },
                new Bird { AnimalId = Guid.NewGuid(), Name = "Bird2" },
            };

            _mockRepository.Setup(repo => repo.GetAllBirdsAsync()).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Bird>>());
            Assert.That(result.Count, Is.EqualTo(expectedBirds.Count));

            // Du kan även göra specifika kontroller för fåglarna om det behövs
            // Exempel:
            Assert.That(result[0].AnimalId, Is.EqualTo(expectedBirds[0].AnimalId));
            Assert.That(result[0].Name, Is.EqualTo(expectedBirds[0].Name));
            // Fortsätt för resten av attributen om det behövs
        }
    }
}
