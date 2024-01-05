using Moq;
using Application.Commands.User.UpdateUser;
using Application.Dtos;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;

[TestFixture]
public class UpdateUserTests
{
    private UpdateUserByIdCommandHandler _handler;
    private Mock<IUserInterface> _mockRepository;
    private Mock<ILogger<UpdateUserByIdCommandHandler>> _loggerMock; // Add logger mock

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IUserInterface>();
        _loggerMock = new Mock<ILogger<UpdateUserByIdCommandHandler>>(); // Initialize logger mock
        _handler = new UpdateUserByIdCommandHandler(_mockRepository.Object, _loggerMock.Object); // Pass logger mock
    }

    [Test]
    public async Task Handle_WithValidUser_ShouldUpdateAndReturnUser()
    {
        // Arrange
        var existingUser = new UserModel { UserId = Guid.NewGuid(), UserName = "OldName" };
        var updatedUser = new UserModel { UserId = existingUser.UserId, UserName = "NewName" };

        _mockRepository.Setup(r => r.GetByIdAsync(existingUser.UserId)).ReturnsAsync(existingUser);

        var command = new UpdateUserByIdCommand(
            new UserDto { UserName = "NewName", Password = "NewPassword" },
            existingUser.UserId
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(existingUser.UserId));
        Assert.That(result.UserName, Is.EqualTo("NewName"));

        // Log the expected and actual user IDs
        Console.WriteLine($"Expected User ID: {existingUser.UserId}");
        Console.WriteLine($"Actual User ID in Result: {result.UserId}");
    }
}