using Application.Commands.Dogs;
using Application.Dtos;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.Commands.Dogs
{
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        [Test]
        public async Task Handle_AddDogToDatabase()
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

            var handler = new AddDogCommandHandler(configurationMock.Object, dbContext);

            var request = new AddDogCommand(new DogDto
            {
                Name = "TestDog",
                BreedDog = "Labrador",
                WeightDog = 25
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the dog is added to the in-memory database
            Assert.That(dbContext.Dogs.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Dogs.First().Name, Is.EqualTo(request.NewDog.Name));
            Assert.That(dbContext.Dogs.First().BreedDog, Is.EqualTo(request.NewDog.BreedDog));
            Assert.That(dbContext.Dogs.First().WeightDog, Is.EqualTo(request.NewDog.WeightDog));
        }
    }
}