using Application.Queries.Cats;
using Domain.Models;
using Infrastructure.DataDbContex;
using Moq;
using Application.Queries.Cats.GetAll;
using Infrastructure.DataDbContex.Interfaces;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTests
    {
        private GetAllCatsQueryHandler _handler;
        private Mock<IAnimalsRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            // Använd Moq för att skapa en generisk mock av IAnimalsRepository
            _mockRepository = new Mock<IAnimalsRepository>();
            _handler = new GetAllCatsQueryHandler(_mockRepository.Object);
        }

        [Test]
        public async Task ShouldReturnAllCats()
        {
            // Arrange
            // Arrange
            var query = new GetAllCatsQuery();
            var expectedCats = new List<Cat>
            {
                new Cat { AnimalId = Guid.NewGuid(), Name = "Cat1" },
                new Cat { AnimalId = Guid.NewGuid(), Name = "Cat2" },
            };

            _mockRepository.Setup(repo => repo.GetAllCatsAsync()).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Cat>>());
            Assert.That(result.Count, Is.EqualTo(expectedCats.Count));

            // Du kan även göra specifika kontroller för katterna om det behövs
            // Exempel:
            Assert.That(result[0].AnimalId, Is.EqualTo(expectedCats[0].AnimalId));
            Assert.That(result[0].Name, Is.EqualTo(expectedCats[0].Name));
            // Fortsätt för resten av attributen om det behövs
        }
    }
}