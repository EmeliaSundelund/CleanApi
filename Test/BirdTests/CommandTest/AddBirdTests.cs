using System;
using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;

        //denna ska du kommentera 
        [SetUp]
        public void Setup()
        {
            _handler = new AddBirdCommandHandler(new MockDatabase());
        }

        [Test]
        public async Task AddsBirdToDatabas()
        {
            //kommentera här
            var newBird = new BirdDto { Name = "NewBirdName" };
            var command = new AddBirdCommand(newBird);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Bird>());

            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

            Assert.That(result.Name, Is.EqualTo("NewBirdName"));
        }
    }
}
