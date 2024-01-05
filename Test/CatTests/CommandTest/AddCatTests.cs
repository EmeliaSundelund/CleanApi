using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
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

namespace Tests.Application.Commands.Cats
{
    [TestFixture]
    public class AddCatCommandHandlerTests
    {
        [Test]
        public async Task Handle_AddCatToDatabase()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger<AddCatCommandHandler>>();

            // Use DbContextOptionsBuilder.ConfigureWarnings to silence warnings about the in-memory database
            var options = new DbContextOptionsBuilder<DataDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // Create an actual instance of DataDbContex with the in-memory database
            using var dbContext = new DataDbContex(options);

            var handler = new AddCatCommandHandler(configurationMock.Object, dbContext, loggerMock.Object);

            var request = new AddCatCommand(new CatDto
            {
                Name = "TestCat",
                BreedCat = "Persian",
                WeightCat = 10
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the cat is added to the in-memory database
            Assert.That(dbContext.Cats.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Cats.First().Name, Is.EqualTo(request.NewCat.Name));
            Assert.That(dbContext.Cats.First().BreedCat, Is.EqualTo(request.NewCat.BreedCat));
            Assert.That(dbContext.Cats.First().WeightCat, Is.EqualTo(request.NewCat.WeightCat));
        }
    }
}