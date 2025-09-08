using FluentAssertions;
using MarstonX.Api.Controllers.v1.Users;
using MarstonX.Application.Users.Commands;
using MarstonX.Application.Users.Queries;
using MarstonX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarstonX.Api.Tests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var userId = 1;
        var expectedUser = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedUser);

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(expectedUser);

        _mediatorMock.Verify(m => m.Send(It.Is<GetUserQuery>(q => q.Id == userId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var userId = 999;
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((User)null!);

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAllUsers_ReturnsUsersList()
    {
        // Arrange
        var users = new List<User>
        {
            new() { Id = 1, FirstName = "John", Email = "john@example.com" },
            new() { Id = 2, FirstName = "Jane", Email = "jane@example.com" }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUsers();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task CreateUser_WithValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateUserCommand("santhosh", "Doe", "john@example.com");
        var userId = 1;

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(userId);

        // Act
        var result = await _controller.CreateUser(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.ActionName.Should().Be(nameof(UsersController.GetUser));
        createdResult.RouteValues!["id"].Should().Be(userId);
        createdResult.Value.Should().Be(userId);
    }

    [Fact]
    public async Task UpdateUser_WithValidCommand_ReturnsNoContent()
    {
        // Arrange
        var command = new UpdateUserCommand(1, "John", "Updated", "john@example.com");

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true); // Return true for success

        // Act
        var result = await _controller.UpdateUser(command.Id, command);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateUser_WithNonExistentUser_ReturnsNotFound()
    {
        // Arrange
        var command = new UpdateUserCommand(999, "John", "Updated", "john@example.com");

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false); // Return false for failure

        // Act
        var result = await _controller.UpdateUser(command.Id, command);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteUser_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var userId = 1;

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true); // Return true for success

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteUser_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var userId = 999;

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false); // Return false for failure

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}