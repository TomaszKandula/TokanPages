namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Users;

public class AddUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenAddUser_ShouldSucceed() 
    {
        // Arrange
        var command = new AddUserCommand 
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = string.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            Password = string.Empty
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(4);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(500),
            FirstName = DataUtilityService.GetRandomString(500),
            LastName = DataUtilityService.GetRandomString(500),
            Password = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(4);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }
}