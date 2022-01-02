namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class UpdateUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenAllFieldsAreCorrect_WhenUpdateUser_ShouldFinishSuccessful()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyEmailAddress_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = string.Empty,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongEmailAddress_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = new string('T', 256),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyUserAlias_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = string.Empty,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongUserAlias_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(256),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyFirstName_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = string.Empty,
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongFirstName_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(256),
            LastName = DataUtilityService.GetRandomString()
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyLastName_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = string.Empty
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongLastName_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(256)
        };

        // Act
        var updateUserCommandValidator = new UpdateUserCommandValidator();
        var result = updateUserCommandValidator.Validate(updateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
    }
}