namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class UpdateUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenUpdateUser_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            UserVideoName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new UpdateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.Empty,
            EmailAddress = string.Empty,
            UserAlias = string.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            UserAboutText = string.Empty,
            UserImageName = string.Empty,
            UserVideoName = string.Empty,
        };

        // Act
        var validator = new UpdateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(7);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[6].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(500),
            FirstName = DataUtilityService.GetRandomString(500),
            LastName = DataUtilityService.GetRandomString(500),
            EmailAddress = DataUtilityService.GetRandomEmail(500),
            UserAboutText = DataUtilityService.GetRandomString(500),
            UserImageName = DataUtilityService.GetRandomString(500),
            UserVideoName = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new UpdateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(7);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_USER_IMAGE_NAME));
        result.Errors[6].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_USER_VIDEO_NAME));
    }
}