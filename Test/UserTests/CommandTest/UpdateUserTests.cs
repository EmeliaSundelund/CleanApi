using Moq;
using Application.Commands.User.UpdateUser;
using Application.Dtos;
using Domain.Models;
using Infrastructure.DataDbContex;
using Application.Commands.Dogs.UpdateDog;
using Domain.Models.Person;

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
        var existingUser = new UserModel { UserId = Guid.NewGuid(), UserName = "OldName" };
        var updatedUser = new UserModel { UserId = existingUser.UserId, UserName = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingUser.UserId)).ReturnsAsync(existingUser);

        var command = new UpdateUserByIdCommand(
        new UserDto { UserName = "NewName", Password = "NewPassword" }, // Uppdatera med rätt egenskaper
        existingUser.UserId
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Logga id för existingDog och result
        Console.WriteLine($"existingUser.id: {existingUser.UserId}");
        Console.WriteLine($"result.Id: {result.UserId}");
    }
}