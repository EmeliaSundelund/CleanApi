using Application.Dtos;
using Application.Commands.Dogs.AddDog;
using Infrastructure.Database;
using Domain.Models;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        private AddDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            // Skapa en instans av AddDogCommandHandler med en mock av MockDatabase
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddsDogToDatabase()
        {
            // Arrange
            var newDog = new DogDto { Name = "NewDogName" };
            var command = new AddDogCommand(newDog);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Dog>(result);

            // Kontrollera att hunden har fått ett giltigt ID
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

            // Kontrollera att hunden har rätt namn enligt det som skickades med kommandot
            Assert.That(result.Name, Is.EqualTo("NewDogName"));
        }
    }
}
