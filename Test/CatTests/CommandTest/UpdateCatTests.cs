using Moq;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;

[TestFixture]
public class UpdateCatByIdCommandHandlerTests
{
    private UpdateCatByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _handler = new UpdateCatByIdCommandHandler(_mockRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidCat_ShouldUpdateAndReturnCat()
    {
        // Arrange
        var existingCat = new Cat { id = Guid.NewGuid(), Name = "OldName" };
        var updatedCat = new Cat { id = existingCat.id, Name = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingCat.id)).ReturnsAsync(existingCat);

        var command = new UpdateCatByIdCommand(
            new CatDto { Name = "NewName" },
            existingCat.id
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingBird och result
        Console.WriteLine($"existingCat.id: {existingCat.id}");
        Console.WriteLine($"result.id: {result.id}");
    }
}

