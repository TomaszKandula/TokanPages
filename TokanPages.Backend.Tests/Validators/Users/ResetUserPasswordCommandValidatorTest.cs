namespace TokanPages.Backend.Tests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;

    public class ResetUserPasswordCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenEmailAddress_WhenResetUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var LResetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
            };

            // Act
            var LResetUserPasswordCommandValidator = new ResetUserPasswordCommandValidator();
            var LResults = LResetUserPasswordCommandValidator.Validate(LResetUserPasswordCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenNoEmailAddress_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var LResetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = string.Empty
            };

            // Act
            var LResetUserPasswordCommandValidator = new ResetUserPasswordCommandValidator();
            var LResults = LResetUserPasswordCommandValidator.Validate(LResetUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}