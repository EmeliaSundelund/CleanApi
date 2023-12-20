using Moq;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;

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
        var existingCat = new Cat { AnimalId = Guid.NewGuid(), Name = "OldName" };
        var updatedCat = new Cat { AnimalId = existingCat.AnimalId, Name = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingCat.AnimalId)).ReturnsAsync(existingCat);

        var command = new UpdateCatByIdCommand(
        new CatDto { Name = "NewName", BreedCat = null }, // Ställ in BreedCat som null eller använd rätt värde
        existingCat.AnimalId
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingBird och result
        Console.WriteLine($"existingCat.id: {existingCat.AnimalId}");
        Console.WriteLine($"result.id: {result.AnimalId}");
    }

}