namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class AddUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenAllFieldsAreCorrect_WhenAddUser_ShouldFinishSuccessful() 
    {
        // Arrange
        var addUserCommand = new AddUserCommand 
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyEmailAddress_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = string.Empty,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongEmailAddress_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = new string('T', 256),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyFirstName_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = string.Empty,
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongFirstName_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(256),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyLastName_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = string.Empty,
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongLastName_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(256),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var addUserCommandValidator = new AddUserCommandValidator();
        var result = addUserCommandValidator.Validate(addUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
    }
}