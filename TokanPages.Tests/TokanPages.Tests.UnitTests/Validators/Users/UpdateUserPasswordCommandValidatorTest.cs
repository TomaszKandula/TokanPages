using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class UpdateUserPasswordCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenUpdateUserPassword_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            ResetId = Guid.NewGuid(),
            OldPassword = DataUtilityService.GetRandomString(),
            NewPassword = "QwertyQwerty#2020*"
        };

        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenCurrentAndNewPassword_WhenUpdateUserPassword_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            OldPassword = DataUtilityService.GetRandomString(),
            NewPassword = "QwertyQwerty#2020*"
        };

        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyNewPassword_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            ResetId = Guid.NewGuid(),
            OldPassword = DataUtilityService.GetRandomString(),
            NewPassword = string.Empty
        };
    
        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);
    
        // Assert
        result.Errors.Count.Should().Be(6);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_SHORT));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_CHAR));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_NUMBER));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER));
    }

    [Fact]
    public void GivenNewPasswordTooLong_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            ResetId = Guid.NewGuid(),
            OldPassword = DataUtilityService.GetRandomString(),
            NewPassword = $"Ab{DataUtilityService.GetRandomString(50)}"
        };
    
        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);
    
        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }
}