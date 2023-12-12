using Application.Commands.Birds.DeleteBird;
using Domain.Models;
using Infrastructure.Database;

namespace Tests.Application.Commands.Birds
{/*
    [TestFixture]
    public class DeleteBirdCommandHandlerTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler((Infrastructure.DataDbContex.IAnimalsRepository)_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldDeleteBird()
        {
            // Arrange
            var initialBird = new Bird { id = Guid.NewGuid(), Name = "InitialBirdName" };
            _mockDatabase.Birds.Add(initialBird);

            var command = new DeleteBirdByIdCommand(deletedBirdId: initialBird.id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);

            var deletedBirdInDatabase = _mockDatabase.Dogs.FirstOrDefault(bird => bird.id == command.DeletedBirdId);
            Assert.That(deletedBirdInDatabase, Is.Null);
        }
    }
    */
}
