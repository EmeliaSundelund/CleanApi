using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTests
    {
        private AddDogCommandHandler _handler;

        
        [SetUp]
        public void Setup()
        {
            _handler = new AddDogCommandHandler(new MockDatabase());
        }

        [Test]
        public async Task AddsDogToDatabas()
        {
            
            var newDog = new DogDto { Name = "NewDogName" };
            var command = new AddDogCommand(newDog);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Dog>());

            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

            Assert.That(result.Name, Is.EqualTo("NewDogName"));
        }
    }
}