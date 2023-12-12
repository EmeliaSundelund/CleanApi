using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.Database;

namespace Tests.Application.Commands.Dogs
{/*
    [TestFixture]
    public class DeleteDogCommandHandlerTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler((Infrastructure.DataDbContex.IAnimalsRepository)_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldDeleteDog()
        {
            // Arrange
            var initialDog = new Dog { id = Guid.NewGuid(), Name = "InitialDogName" };
            _mockDatabase.Dogs.Add(initialDog);

            var command = new DeleteDogByIdCommand(deletedDogId: initialDog.id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);

            var deletedDogInDatabase = _mockDatabase.Dogs.FirstOrDefault(dog => dog.id == command.DeletedDogId);
            Assert.That(deletedDogInDatabase, Is.Null);
    
    }
    */
}
