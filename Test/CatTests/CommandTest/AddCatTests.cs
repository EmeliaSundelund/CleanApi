using System;
using Application.Commands.Cats;
using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CatsTests.CommandTest
{/*
    [TestFixture]
    public class AddCatTests
    {
        private AddCatCommandHandler _handler;


        [SetUp]
        public void Setup()
        {
            _handler = new AddCatCommandHandler(new MockDatabase());
        }

        [Test]
        public async Task AddsCatToDatabas()
        {
            //Arrange
            var newCat = new CatDto { Name = "NewCatName" };
            var command = new AddCatCommand(newCat);
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cat>());

            Assert.That(result.id, Is.Not.EqualTo(Guid.Empty));

            Assert.That(result.Name, Is.EqualTo("NewCatName"));
        }
    }
    */
}
