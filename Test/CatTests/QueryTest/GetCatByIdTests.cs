using Moq;
using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;

namespace Application.Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetCatByIdTests
    {
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalsRepository>();
            var expectedCat = new Cat { AnimalId = Guid.NewGuid(), Name = "Buddy" };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedCat);

            var queryHandler = new GetCatByIdQueryHandler(mockRepository.Object);
            var query = new GetCatByIdQuery(expectedCat.AnimalId);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedCat));
        }
    }
}
