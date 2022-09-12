namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Users;

public class RemoveUserCommandValidatorTest
{
    [Fact]
    public void GivenValidId_WhenRemoveUser_ShouldSucceed()
    {
        // Arrange
        var command = new RemoveUserCommand { Id = Guid.NewGuid() };

        // Act
        var validator = new RemoveUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenNullValue_WhenRemoveUser_ShouldSucceed()
    {
        // Arrange
        var command = new RemoveUserCommand { Id = null };

        // Act
        var validator = new RemoveUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenRemoveUser_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveUserCommand { Id = Guid.Empty };

        // Act
        var validator = new RemoveUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}