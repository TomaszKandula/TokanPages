namespace TokanPages.Backend.Tests.Validators.Mailer
{
    using Cqrs.Handlers.Commands.Mailer;
    using Shared.Resources;
    using FluentAssertions;
    using Xunit;

    public class VerifyEmailAddressCommandValidatorTest
    {
        [Fact]
        public void GivenEmail_WhenVerifyEmailAddress_ShouldFinishSuccessful() 
        {
            // Arrange
            var verifyEmailAddressCommand = new VerifyEmailAddressCommand 
            { 
                Email = "tokan@dfds.com"
            };

            // Act
            var validator = new VerifyEmailAddressCommandValidator();
            var result = validator.Validate(verifyEmailAddressCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmail_WhenVerifyEmailAddress_ShouldThrowError()
        {
            // Arrange
            var verifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = string.Empty
            };

            // Act
            var validator = new VerifyEmailAddressCommandValidator();
            var result = validator.Validate(verifyEmailAddressCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongEmail_WhenVerifyEmailAddress_ShouldThrowError()
        {
            // Arrange
            var verifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = new string('T', 256)
            };

            // Act
            var validator = new VerifyEmailAddressCommandValidator();
            var result = validator.Validate(verifyEmailAddressCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }
    }
}