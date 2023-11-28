using System;
using Application.Commands.Birds.DeleteBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;


namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class DeleteCatTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteBirdInDatabase()
        {
            var initialBird = new Bird { Id = Guid.NewGuid(), Name = "InitialBirdName" };
            _mockDatabase.Birds.Add(initialBird);

            var command = new DeleteBirdByIdCommand(deletedBird: new BirdDto { Name = "InitialBirdName" }, deletedBirdId: initialBird.Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.True);

            var deletedBirdInDatabase = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == command.DeletedBirdId);
            Assert.That(deletedBirdInDatabase, Is.Null);
        }
    }
}