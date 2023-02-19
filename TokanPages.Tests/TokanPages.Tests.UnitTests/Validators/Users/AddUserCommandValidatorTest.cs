using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

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
            Password = "Test#2023*"
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
        result.Errors.Count.Should().Be(9);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_SHORT));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_CHAR));
        result.Errors[6].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_NUMBER));
        result.Errors[7].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER));
        result.Errors[8].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER));
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
            Password = $"Ab{DataUtilityService.GetRandomString(50)}"
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

    [Fact]
    public void GivenTooShortPassword_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = "Abc#20"
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_SHORT));
    }

    [Fact]
    public void GivenPasswordMissingChar_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = "Abcd2020"
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_CHAR));
    }

    [Fact]
    public void GivenPasswordMissingNumber_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = "Abcd#Abcd$"
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_NUMBER));
    }

    [Fact]
    public void GivenPasswordMissingLargeLetter_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = "abcd#2020$"
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER));
    }

    [Fact]
    public void GivenPasswordMissingSmallLetter_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = "ABCD#2020$"
        };

        // Act
        var validator = new AddUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER));
    }
}