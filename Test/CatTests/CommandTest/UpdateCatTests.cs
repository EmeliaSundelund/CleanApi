using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;


namespace Test.CatTests.CommandTest
{/*
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler((Infrastructure.DataDbContex.IAnimalsRepository)_mockDatabase);
        }

        [Test]
        public async Task UpdateCatInDatabase()
        {
            //Arrange
            var initialCat = new Cat { id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            var command = new UpdateCatByIdCommand(updatedCat: new CatDto { Name = "UpdatedCatName" }, id: initialCat.id);
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cat>());

            Assert.That(result.Name, Is.EqualTo("UpdatedCatName"));

            var updatedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.id == command.Id);
            Assert.That(updatedCatInDatabase, Is.Not.Null);
            Assert.That(updatedCatInDatabase.Name, Is.EqualTo("UpdatedCatName"));
        }
    }
    */
}