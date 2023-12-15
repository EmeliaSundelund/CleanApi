using Application.Commands.Dogs;
using Application.Commands.User.AddUser;
using Application.Dtos;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.UserTests.CommandTest
{
    [TestFixture]
    public class AddUserTests
    {
        [Test]
        public async Task Handle_AddUserToDatabase()
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

            var handler = new AddUserCommandHandler(configurationMock.Object, dbContext);

            var request = new AddUserCommand(new UserDto
            {
                UserName = "TestDog",
                Password = "Labrador",
                Animals = 25
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the dog is added to the in-memory database
            Assert.That(dbContext.Users.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Users.First().UserName, Is.EqualTo(request.NewUser.UserName));
            Assert.That(dbContext.Users.First().Password, Is.EqualTo(request.NewUser.Password));
            Assert.That(dbContext.Users.First().Animals, Is.EqualTo(request.NewUser.Animals));
        }
    }
}
