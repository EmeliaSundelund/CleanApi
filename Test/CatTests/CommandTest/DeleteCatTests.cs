using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.Database;

namespace Tests.Application.Commands.Cats
{/*
    [TestFixture]
    public class DeleteCatCommandHandlerTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatByIdCommandHandler((Infrastructure.DataDbContex.IAnimalsRepository)_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldDeleteCat()
        {
            // Arrange
            var initialCat = new Cat { id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            var command = new DeleteCatByIdCommand(deletedCatId: initialCat.id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);

            var deletedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.id == command.DeletedCatId);
            Assert.That(deletedCatInDatabase, Is.Null);
        }
    }
    */
}
