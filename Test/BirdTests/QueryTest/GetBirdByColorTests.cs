using Moq;
using Application.Queries.Birds.GetAllColor;
using Domain.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DataDbContex;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByColorTests
    {
        [Test]
        public async Task Handle_ReturnsSortedBirdsByColorAndName()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalsRepository>();
            var expectedBirds = new List<Bird>
            {
                new Bird { id = Guid.NewGuid(), Name = "Robin", Color = "Blue" },
                new Bird { id = Guid.NewGuid(), Name = "Blue Jay", Color = "Blue" },
                new Bird { id = Guid.NewGuid(), Name = "Canary", Color = "Yellow" }
            };

            expectedBirds = expectedBirds.OrderByDescending(b => b.Name).ThenBy(b => b.Color).ToList();

            mockRepository.Setup(repo => repo.GetBirdsByColorAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedBirds);

            var queryHandler = new GetBirdsByColorQueryHandler(mockRepository.Object);
            var query = new GetBirdsByColorQuery("Blue");

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.InstanceOf<List<Bird>>());
            Assert.That(result.Count, Is.EqualTo(expectedBirds.Count));

            // Skapa en anpassad jämförare för Bird
            var birdComparer = Comparer<Bird>.Create((x, y) =>
            {
                // Jämför efter namn (Name) och sedan efter färg (Color)
                int nameComparison = y.Name.CompareTo(x.Name);
                return nameComparison != 0 ? nameComparison : x.Color.CompareTo(y.Color);
            });

            // Verifiera ordning efter namn och färg med anpassad jämförare
            CollectionAssert.AreEqual(expectedBirds, result, birdComparer);
        }
    }
}
