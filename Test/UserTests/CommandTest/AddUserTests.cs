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
                UserName = "emma",
                Password = "emma",
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);

            // Verify that the user is added to the in-memory database
            Assert.That(dbContext.Person.Count(), Is.EqualTo(1));
            var userInDb = dbContext.Person.First();
            Assert.That(userInDb.UserName, Is.EqualTo(request.NewUser.UserName));

            // Log the expected and actual password hashes
            Console.WriteLine($"Expected Password Hash: {request.NewUser.Password}");
            Console.WriteLine($"Actual Password Hash in DB: {userInDb.Password}");

            // Hash the expected password using the same method and parameters as in AddUserCommandHandler
            var expectedPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password, userInDb.Password);
            Assert.That(userInDb.Password, Is.EqualTo(expectedPasswordHash));

        }
    }
}