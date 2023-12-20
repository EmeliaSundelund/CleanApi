using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.Commands.Cats
{
    [TestFixture]
    public class AddCatsCommandHandlerTests
    {
        [Test]
        public async Task Handle_AddCatToDatabase()
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

            var handler = new AddCatCommandHandler(configurationMock.Object, dbContext);

            var request = new AddCatCommand(new CatDto
            {
                Name = "TestCat",
                BreedCat = "NakenKatt",
                WeightCat = 25
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the dog is added to the in-memory database
            Assert.That(dbContext.Cats.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Cats.First().Name, Is.EqualTo(request.NewCat.Name));
            Assert.That(dbContext.Cats.First().BreedCat, Is.EqualTo(request.NewCat.BreedCat));
            Assert.That(dbContext.Cats.First().WeightCat, Is.EqualTo(request.NewCat.WeightCat));
        }
    }
}