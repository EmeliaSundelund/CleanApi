using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.Commands.Birds
{
    [TestFixture]
    public class AddBirdsCommandHandlerTests
    {
        [Test]
        public async Task Handle_AddBirdToDatabase()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();

            // Använd DbContextOptionsBuilder.ConfigureWarnings för att tysta varningar om in-memory-databasen
            var options = new DbContextOptionsBuilder<DataDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // Skapa en faktisk instans av DataDbContex med in-memory-databasen
            using var dbContext = new DataDbContex(options);

            var handler = new AddBirdCommandHandler(configurationMock.Object, dbContext);

            var request = new AddBirdCommand(new BirdDto
            {
                Name = "TestDog",
                Color = "Blue",
                CanFly = true,
           
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the dog is added to the in-memory database
            Assert.That(dbContext.Birds.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Birds.First().Name, Is.EqualTo(request.NewBird.Name));
            Assert.That(dbContext.Birds.First().Color, Is.EqualTo(request.NewBird.Color));
            Assert.That(dbContext.Birds.First().CanFly, Is.EqualTo(request.NewBird.CanFly));
      
        }
    }
}