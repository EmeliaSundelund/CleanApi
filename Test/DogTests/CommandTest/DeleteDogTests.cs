﻿using System;
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
        public async Task DeleteDogInDatabas()
        {
            var initialDog = new Dog { Id = Guid.NewGuid(), Name = "InitialDogName" };
            _mockDatabase.Dogs.Add(initialDog);

            var command = new DeleteDogByIdCommand(deletedDog: new DogDto { Name = "InitialDogName" },deletedDogId: initialDog.Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);

            var deletedDogInDatabase = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == command.DeletedDogId);
            Assert.IsNull(deletedDogInDatabase);
        }
    }
}