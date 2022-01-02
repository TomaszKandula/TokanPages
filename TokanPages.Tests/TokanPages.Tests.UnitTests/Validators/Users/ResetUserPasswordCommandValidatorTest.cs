namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class ResetUserPasswordCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenEmailAddress_WhenResetUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
        };

        // Act
        var resetUserPasswordCommandValidator = new ResetUserPasswordCommandValidator();
        var result = resetUserPasswordCommandValidator.Validate(resetUserPasswordCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenNoEmailAddress_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            EmailAddress = string.Empty
        };

        // Act
        var resetUserPasswordCommandValidator = new ResetUserPasswordCommandValidator();
        var result = resetUserPasswordCommandValidator.Validate(resetUserPasswordCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}