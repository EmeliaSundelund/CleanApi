using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteDogInDatabase()
        {
            //Arrange
            var initialDog = new Dog { id = Guid.NewGuid(), Name = "InitialDogName" };
            _mockDatabase.Dogs.Add(initialDog);

            var command = new DeleteDogByIdCommand(deletedDog: new DogDto { Name = "InitialDogName" }, deletedDogId: initialDog.id);
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.That(result, Is.True);

            var deletedDogInDatabase = _mockDatabase.Dogs.FirstOrDefault(dog => dog.id == command.DeletedDogId);
            Assert.That(deletedDogInDatabase, Is.Null);
        }
    }
}