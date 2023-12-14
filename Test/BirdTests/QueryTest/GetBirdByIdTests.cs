using Moq;
using Application.Queries.Birds.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalsRepository>();
            var expectedCat = new Bird { id = Guid.NewGuid(), Name = "Buddy" };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedCat);

            var queryHandler = new GetBirdByIdQueryHandler(mockRepository.Object);
            var query = new GetBirdByIdQuery(expectedCat.id);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCat, result);
        }
    }
}
