using NUnit.Framework;
using Moq;
using Application.Commands.Bird.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class UpdateBirdByIdCommandHandlerTests
{
    private UpdateBirdByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;
    private Mock<ILogger<UpdateBirdByIdCommandHandler>> _loggerMock; // Add logger mock

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _loggerMock = new Mock<ILogger<UpdateBirdByIdCommandHandler>>(); // Initialize logger mock
        _handler = new UpdateBirdByIdCommandHandler(_mockRepository.Object, _loggerMock.Object); // Pass logger mock
    }

    [Test]
    public async Task Handle_WithValidBird_ShouldUpdateAndReturnBird()
    {
        // Arrange
        var existingBird = new Bird { AnimalId = Guid.NewGuid(), Name = "OldName", CanFly = false };
        var updatedBird = new Bird { AnimalId = existingBird.AnimalId, Name = "NewName", CanFly = true };

        _mockRepository.Setup(r => r.GetByIdAsync(existingBird.AnimalId)).ReturnsAsync(existingBird);

        var command = new UpdateBirdByIdCommand(
            new BirdDto { Name = "NewName", Color = "NewColor", CanFly = false },
            existingBird.AnimalId
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AnimalId, Is.EqualTo(existingBird.AnimalId));
        Assert.That(result.Name, Is.EqualTo("NewName"));

        // Log the expected and actual bird IDs
        Console.WriteLine($"Expected Bird ID: {existingBird.AnimalId}");
        Console.WriteLine($"Actual Bird ID in Result: {result.AnimalId}");
    }
}