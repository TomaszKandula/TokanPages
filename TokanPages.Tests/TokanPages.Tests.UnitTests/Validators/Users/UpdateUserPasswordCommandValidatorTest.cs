namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class UpdateUserPasswordCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenUserIdAndNewPassword_WhenUpdateUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenNoUserIdAndNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.Empty,
            NewPassword = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenUserIdAndNoNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = string.Empty
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenNoUserIdAndNoNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.Empty,
            NewPassword = string.Empty
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenUserIdAndTooLongNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString(500)
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }

    [Fact]
    public void GivenNoUserIdAndTooLongNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.Empty,
            NewPassword = DataUtilityService.GetRandomString(500)
        };

        // Act
        var updateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
        var result = updateUserPasswordCommandValidator.Validate(updateUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }
}