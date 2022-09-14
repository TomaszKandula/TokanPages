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
            NewPassword = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.Empty,
            NewPassword = string.Empty
        };

        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongString_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new UpdateUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }
}