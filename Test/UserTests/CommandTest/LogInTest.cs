using Application.Commands.User.LogInUser;
using Application.Dtos;
using Domain.Models.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Application.Tests.Commands.User.LogInUser
{
    [TestFixture]
    public class LogInCommandHandlerTests
    {
        [Test]
        public async Task Handle_LogIn_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Infrastructure.DataDbContex.DataDbContex>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new Infrastructure.DataDbContex.DataDbContex(options);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("AppSettings:TokenKey").Value).Returns("your_secret_key_with_at_least_128_bits");

            var handler = new LogInCommandHandler(configurationMock.Object, dbContext);

            // Add test user to in-memory database
            var user = new UserModel
            {
                UserName = "testUser",
                Password = BCrypt.Net.BCrypt.HashPassword("testPassword")
            };
            dbContext.Person.Add(user);
            dbContext.SaveChanges();

            var logInDto = new UserDto
            {
                UserName = "testUser",
                Password = "testPassword"
            };

            var logInCommand = new LogInCommand(logInDto);

            // Act
            var result = await handler.Handle(logInCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions based on your requirements
        }
    }

}