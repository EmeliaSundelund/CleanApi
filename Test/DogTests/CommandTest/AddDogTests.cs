using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.Commands.Dogs
{/*
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_ShouldCreateDog()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var dataDbContextMock = new Mock<DataDbContex>();

            var handler = new AddDogCommandHandler(configurationMock.Object, dataDbContextMock.Object);

            var request = new AddDogCommand(new DogDto
            {
                Name = "TestDog",
                BreedDog = "Labrador",
                WeightDog = 25
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(request.NewDog.Name));
            Assert.That(result.BreedDog, Is.EqualTo(request.NewDog.BreedDog));
            Assert.That(result.WeightDog, Is.EqualTo(request.NewDog.WeightDog));

            // Verify that AddAsync and SaveChangesAsync were called once
            dataDbContextMock.Verify(db => db.Dogs.AddAsync(It.IsAny<Dog>(), CancellationToken.None), Times.Once);
            dataDbContextMock.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
    */
}
