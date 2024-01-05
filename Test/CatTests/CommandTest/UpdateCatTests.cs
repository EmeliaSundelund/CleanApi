using NUnit.Framework;
using Moq;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class UpdateCatByIdCommandHandlerTests
{
    private UpdateCatByIdCommandHandler _handler;
    private Mock<IAnimalsRepository> _mockRepository;
    private Mock<ILogger<UpdateCatByIdCommandHandler>> _loggerMock; // Lägg till logger mock

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IAnimalsRepository>();
        _loggerMock = new Mock<ILogger<UpdateCatByIdCommandHandler>>(); // Initialisera logger mock
        _handler = new UpdateCatByIdCommandHandler(_mockRepository.Object, _loggerMock.Object); // Skicka logger mock som parameter
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

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AnimalId, Is.EqualTo(existingCat.AnimalId));
        Assert.That(result.Name, Is.EqualTo("NewName"));

        // Logga förväntat och faktiskt katt-ID
        Console.WriteLine($"Expected Cat ID: {existingCat.AnimalId}");
        Console.WriteLine($"Actual Cat ID in Result: {result.AnimalId}");
    }
}