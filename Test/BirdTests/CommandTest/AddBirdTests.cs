using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.Commands.Birds
{/*
    [TestFixture]
    public class AddBirdCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_ShouldCreateBird()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var dataDbContextMock = new Mock<DataDbContex>();

            var handler = new AddBirdCommandHandler(configurationMock.Object, dataDbContextMock.Object);

            var request = new AddBirdCommand(new BirdDto
            {
                Name = "Amanda",
                Color = "Blue",
                
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(request.NewBird.Name));
            Assert.That(result.Color, Is.EqualTo(request.NewBird.Color));

            // Verify that AddAsync and SaveChangesAsync were called once
            dataDbContextMock.Verify(db => db.Dogs.AddAsync(It.IsAny<Dog>(), CancellationToken.None), Times.Once);
            dataDbContextMock.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
    */
}
