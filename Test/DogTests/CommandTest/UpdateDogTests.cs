using Moq;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class UpdateDogByIdCommandHandlerTests
{
    private UpdateDogByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;
    private Mock<ILogger<UpdateDogByIdCommandHandler>> _loggerMock; // Add logger mock

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _loggerMock = new Mock<ILogger<UpdateDogByIdCommandHandler>>(); // Initialize logger mock
        _handler = new UpdateDogByIdCommandHandler(_mockRepository.Object, _loggerMock.Object); // Pass logger mock
    }

    [Test]
    public async Task Handle_WithValidDog_ShouldUpdateAndReturnDog()
    {
        // Arrange
        var existingDog = new Dog { AnimalId = Guid.NewGuid(), Name = "OldName" };
        var updatedDog = new Dog { AnimalId = existingDog.AnimalId, Name = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingDog.AnimalId)).ReturnsAsync(existingDog);

        var command = new UpdateDogByIdCommand(
            new DogDto { Name = "NewName" },
            existingDog.AnimalId
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AnimalId, Is.EqualTo(existingDog.AnimalId));
        Assert.That(result.Name, Is.EqualTo("NewName"));

        // Log the expected and actual dog IDs
        Console.WriteLine($"Expected Dog ID: {existingDog.AnimalId}");
        Console.WriteLine($"Actual Dog ID in Result: {result.AnimalId}");
    }
}