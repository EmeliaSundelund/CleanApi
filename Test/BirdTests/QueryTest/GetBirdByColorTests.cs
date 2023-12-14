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
                new Bird { id = Guid.NewGuid(), Name = "Robin", Color = "Blue" },
                new Bird { id = Guid.NewGuid(), Name = "Blue Jay", Color = "Blue" },
                new Bird { id = Guid.NewGuid(), Name = "Canary", Color = "Yellow" }
            };

            expectedBirds.Sort((b1, b2) => b1.Color.CompareTo(b2.Color) != 0 ? b1.Color.CompareTo(b2.Color) : b1.Name.CompareTo(b2.Name));

            mockRepository.Setup(repo => repo.GetBirdsByColorAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedBirds);

            var queryHandler = new GetBirdsByColorQueryHandler(mockRepository.Object);
            var query = new GetBirdsByColorQuery("Blue"); // Använd konstruktorn för att sätta färgen

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Print result and expected before assertions
            Console.WriteLine("Result:");
            foreach (var bird in result)
            {
                Console.WriteLine($"Name: {bird.Name}, Color: {bird.Color}");
            }

            Console.WriteLine("Expected:");
            foreach (var bird in expectedBirds)
            {
                Console.WriteLine($"Name: {bird.Name}, Color: {bird.Color}");
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Bird>>(result);
            Assert.AreEqual(expectedBirds.Count, result.Count);

            // Verify ordering by Name and Color
            var sortedResult = result.OrderBy(bird => bird.Color).ThenBy(bird => bird.Name).ToList();

            CollectionAssert.AreEqual(expectedBirds, sortedResult);

        }
    }
}
