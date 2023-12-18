using Moq;
using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;

namespace Application.Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectDog()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalsRepository>();
            var expectedDog = new Dog { AnimalId = Guid.NewGuid(), Name = "Buddy" };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedDog);

            var queryHandler = new GetDogByIdQueryHandler(mockRepository.Object);
            var query = new GetDogByIdQuery(expectedDog.AnimalId);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedDog));
        }
    }
}
