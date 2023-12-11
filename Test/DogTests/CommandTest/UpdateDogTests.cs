using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task UpdateDogInDatabase()
        {
            //Arrange
            var initialDog = new Dog { id = Guid.NewGuid(), Name = "InitialDogName" };
            _mockDatabase.Dogs.Add(initialDog);

            var command = new UpdateDogByIdCommand(updatedDog: new DogDto { Name = "UpdatedDogName" }, id: initialDog.id);
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Dog>());

            Assert.That(result.Name, Is.EqualTo("UpdatedDogName"));

            var updatedDogInDatabase = _mockDatabase.Dogs.FirstOrDefault(dog => dog.id == command.Id);
            Assert.That(updatedDogInDatabase, Is.Not.Null);
            Assert.That(updatedDogInDatabase.Name, Is.EqualTo("UpdatedDogName"));
        }
    }
}
