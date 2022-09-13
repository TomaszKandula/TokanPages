using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class ResetUserPasswordCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInput_WhenResetUserPassword_ShouldSucceed()
    {
        // Arrange
        var command = new ResetUserPasswordCommand { EmailAddress = DataUtilityService.GetRandomEmail() };

        // Act
        var validator = new ResetUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInput_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        var command = new ResetUserPasswordCommand { EmailAddress = string.Empty };

        // Act
        var validator = new ResetUserPasswordCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}