namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

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