namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Users;

public class RevokeUserRefreshTokenCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInput_WhenRevokeUserRefreshToken_ShouldSucceed()
    {
        // Arrange
        var command = new RevokeUserRefreshTokenCommand { RefreshToken = DataUtilityService.GetRandomString(100) };

        // Act
        var validator = new RevokeUserRefreshTokenCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInput_WhenRevokeUserRefreshToken_ShouldThrowError()
    {
        // Arrange
        var command = new RevokeUserRefreshTokenCommand { RefreshToken = string.Empty };

        // Act
        var validator = new RevokeUserRefreshTokenCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}