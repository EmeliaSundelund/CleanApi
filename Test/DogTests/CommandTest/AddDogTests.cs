using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Microsoft.Extensions.Configuration;

namespace Test.DogTests.CommandTest
{/*
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        private AddDogCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            // Använder en Mock för databasen eller DbContext
            _handler = new AddDogCommandHandler(new Mock<IConfiguration>().Object, new Mock<DataDbContex>().Object);
        }

        [Test]
        public async Task AddsDogToDatabase()
        {
            // Arrange
            var newDog = new DogDto { Name = "NewDogName", BreedDog = "NewBreed", WeightDog = 15.5 };
            var command = new AddDogCommand(newDog);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Dog>());

            Assert.That(result.id, Is.Not.EqualTo(Guid.Empty));

            Assert.That(result.Name, Is.EqualTo("NewDogName"));
            Assert.That(result.BreedDog, Is.EqualTo("NewBreed"));
            Assert.That(result.WeightDog, Is.EqualTo(15.5));
        }
    }
    */
}
