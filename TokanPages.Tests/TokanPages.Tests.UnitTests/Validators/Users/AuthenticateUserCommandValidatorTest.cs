using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class AuthenticateUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AuthenticateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void GivenInvalidInputs_WhenAuthenticateUser_ShouldThrowError(string? emailAddress, string? password)
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            EmailAddress = emailAddress,
            Password = password
        };

        // Act
        var validator = new AuthenticateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(300),
            Password = DataUtilityService.GetRandomString(150)
        };

        // Act
        var validator = new AuthenticateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
    }
}