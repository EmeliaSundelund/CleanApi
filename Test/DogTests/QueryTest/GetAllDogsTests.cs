using Application.Queries.Dogs;
using Domain.Models;
using Infrastructure.DataDbContex;
using Moq;
using Application.Queries.Dogs.GetAll;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private GetAllDogsQueryHandler _handler;
        private Mock<IAnimalsRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            // Använd Moq för att skapa en generisk mock av IAnimalsRepository
            _mockRepository = new Mock<IAnimalsRepository>();
            _handler = new GetAllDogsQueryHandler(_mockRepository.Object);
        }

        [Test]
        public async Task ShouldReturnAllDogs()
        {
            // Arrange
            var query = new GetAllDogsQuery();
            var expectedDogs = new List<Dog>
            {
                new Dog { AnimalId = Guid.NewGuid(), Name = "Dog1" },
                new Dog { AnimalId = Guid.NewGuid(), Name = "Dog2" },
            };

            _mockRepository.Setup(repo => repo.GetAllDogsAsync()).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Dog>>());
            Assert.That(result.Count, Is.EqualTo(expectedDogs.Count));

            // Du kan även göra specifika kontroller för hundarna om det behövs
            // Exempel:
            Assert.That(result[0].AnimalId, Is.EqualTo(expectedDogs[0].AnimalId));
            Assert.That(result[0].Name, Is.EqualTo(expectedDogs[0].Name));
            // Fortsätt för resten av attributen om det behövs
        }
    }
}
