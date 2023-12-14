using Moq;
using Application.Queries.Birds.GetAllColor;
using Domain.Models;
using Infrastructure.DataDbContex;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByColorTests
    {
        [Test]
        public async Task Handle_ReturnsSortedBirdsByColor()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalsRepository>();
            var expectedBirds = new List<Bird>
            {
                new Bird { id = Guid.NewGuid(), Name = "Robin", Color = "Red" },
                new Bird { id = Guid.NewGuid(), Name = "Blue Jay", Color = "Blue" },
                new Bird { id = Guid.NewGuid(), Name = "Canary", Color = "Yellow" }
            };

            mockRepository.Setup(repo => repo.GetBirdsByColorAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedBirds);

            var queryHandler = new GetBirdsByColorQueryHandler(mockRepository.Object);
            var query = new GetBirdsByColorQuery("Red"); // Använd konstruktorn för att sätta färgen

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Bird>>(result);
            Assert.AreEqual(expectedBirds.Count, result.Count);

            // Verify ordering by Name
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.IsTrue(string.Compare(result[i].Name, result[i + 1].Name) >= 0);
            }
        }
    }
}
