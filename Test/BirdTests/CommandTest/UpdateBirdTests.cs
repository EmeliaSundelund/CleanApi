using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task UpdateBirdInDatabase()
        {
            var initialBird = new Bird { Id = Guid.NewGuid(), Name = "InitialBirdName" };
            _mockDatabase.Birds.Add(initialBird);

            var command = new UpdateBirdByIdCommand(updatedBird: new BirdDto { Name = "UpdatedBirdName" }, id: initialBird.Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Bird>());

            Assert.That(result.Name, Is.EqualTo("UpdatedBirdName"));

            var updatedBirdInDatabase = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == command.Id);
            Assert.That(updatedBirdInDatabase, Is.Not.Null);
            Assert.That(updatedBirdInDatabase.Name, Is.EqualTo("UpdatedBirdName"));
        }
    }
}
