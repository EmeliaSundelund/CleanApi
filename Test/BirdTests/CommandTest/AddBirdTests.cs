using Application.Commands.Bird;
using Application.Commands.Bird.AddBird;
using Application.Dtos;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application.Commands.Birds
{
    [TestFixture]
    public class AddBirdTests
    {
        [Test]
        public async Task Handle_AddBirdToDatabase()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger<AddBirdCommandHandler>>(); // Add logger mock

            // Use DbContextOptionsBuilder.ConfigureWarnings to silence warnings about the in-memory database
            var options = new DbContextOptionsBuilder<DataDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // Create an actual instance of DataDbContex with the in-memory database
            using var dbContext = new DataDbContex(options);

            var handler = new AddBirdCommandHandler(configurationMock.Object, dbContext, loggerMock.Object);

            var request = new AddBirdCommand(new BirdDto
            {
                Name = "TestBird",
                Color = "Blue",
                CanFly = true
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the bird is added to the in-memory database
            Assert.That(dbContext.Birds.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Birds.First().Name, Is.EqualTo(request.NewBird.Name));
            Assert.That(dbContext.Birds.First().Color, Is.EqualTo(request.NewBird.Color));
            Assert.That(dbContext.Birds.First().CanFly, Is.EqualTo(request.NewBird.CanFly));
        }
    }
}