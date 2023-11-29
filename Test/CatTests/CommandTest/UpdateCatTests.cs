using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;


namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task UpdateCatInDatabase()
        {
            var initialCat = new Cat { Id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            var command = new UpdateCatByIdCommand(updatedCat: new CatDto { Name = "UpdatedCatName" }, id: initialCat.Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cat>());

            Assert.That(result.Name, Is.EqualTo("UpdatedCatName"));

            var updatedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == command.Id);
            Assert.That(updatedCatInDatabase, Is.Not.Null);
            Assert.That(updatedCatInDatabase.Name, Is.EqualTo("UpdatedCatName"));
        }
    }
}