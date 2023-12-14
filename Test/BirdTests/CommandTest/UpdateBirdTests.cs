using NUnit.Framework;
using Moq;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;

[TestFixture]
public class UpdateBirdByIdCommandHandlerTests
{
    private UpdateBirdByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _handler = new UpdateBirdByIdCommandHandler(_mockRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidBird_ShouldUpdateAndReturnBird()
    {
        // Arrange
        var existingBird = new Bird { id = Guid.NewGuid(), Name = "OldName", CanFly = false };
        var updatedBird = new Bird { id = existingBird.id, Name = "NewName", CanFly = true };

        _mockRepository.Setup(r => r.GetByIdAsync(existingBird.id)).ReturnsAsync(existingBird);

        var command = new UpdateBirdByIdCommand(
            new BirdDto { Name = "NewName", Color = "NewColor", CanFly = false },
            existingBird.id
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingBird och result
        Console.WriteLine($"existingBird.id: {existingBird.id}");
        Console.WriteLine($"result.id: {result.id}");
    }
}
