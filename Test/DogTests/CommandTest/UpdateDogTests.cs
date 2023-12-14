using Moq;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;

[TestFixture]
public class UpdateDogByIdCommandHandlerTests
{
    private UpdateDogByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _handler = new UpdateDogByIdCommandHandler(_mockRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidDog_ShouldUpdateAndReturnDog()
    {
        // Arrange
        var existingDog = new Dog { id = Guid.NewGuid(), Name = "OldName" };
        var updatedDog = new Dog { id = existingDog.id, Name = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingDog.id)).ReturnsAsync(existingDog);

        var command = new UpdateDogByIdCommand(
            new DogDto { Name = "NewName" },
            existingDog.id
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingDog och result
        Console.WriteLine($"existingDog.id: {existingDog.id}");
        Console.WriteLine($"result.id: {result.id}");
    }
}
