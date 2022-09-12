namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Users;

public class ReAuthenticateUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInput_WhenReAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var command = new ReAuthenticateUserCommand { RefreshToken = DataUtilityService.GetRandomString() };

        // Act
        var validator = new ReAuthenticateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var command = new ReAuthenticateUserCommand { RefreshToken = string.Empty };

        // Act
        var validator = new ReAuthenticateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}