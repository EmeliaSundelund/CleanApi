using Moq;
using Application.Commands.User.UpdateUser;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Application.Commands.Dogs.UpdateDog;

[TestFixture]
public class UpdateUserTests
{
    private UpdateUserByIdCommandHandler _handler;
    private Mock<UserInterface> _mockRepository;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<UserInterface>();
        _handler = new UpdateUserByIdCommandHandler(_mockRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidUser_ShouldUpdateAndReturnUser()
    {
        // Arrange
        var existingUser = new UserS { Id = Guid.NewGuid(), UserName = "OldName" };
        var updatedDog = new UserS { Id = existingUser.Id, UserName = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

        var command = new UpdateUserByIdCommand(
        new UserDto { UserName = "NewName", Password = "NewPassword" }, // Uppdatera med rätt egenskaper
        existingUser.Id
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingDog och result
        Console.WriteLine($"existingUser.id: {existingUser.Id}");
        Console.WriteLine($"result.Id: {result.Id}");
    }
}