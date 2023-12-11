using Application.Commands.Cats.DeleteCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;


namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteCatInDatabase()
        {
            //Arrange
            var initialCat = new Cat { id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            var command = new DeleteCatByIdCommand(deletedCat: new CatDto { Name = "InitialCatName" }, deletedCatId: initialCat.id);
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.That(result, Is.True);

            var deletedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.id == command.DeletedCatId);
            Assert.That(deletedCatInDatabase, Is.Null);
        }
    }
}