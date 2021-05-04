using Xunit;
using FluentAssertions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Shared.Resources;

namespace Backend.UnitTests.Validators.Mailer
{
    public class VerifyEmailAddressCommandValidatorTest
    {
        [Fact]
        public void GivenEmail_WhenVerifyEmailAddress_ShouldFinishSuccessful() 
        {
            // Arrange
            var LVerifyEmailAddressCommand = new VerifyEmailAddressCommand 
            { 
                Email = "tokan@dfds.com"
            };

            // Act
            var LValidator = new VerifyEmailAddressCommandValidator();
            var LResult = LValidator.Validate(LVerifyEmailAddressCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmail_WhenVerifyEmailAddress_ShouldThrowError()
        {
            // Arrange
            var LVerifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = string.Empty
            };

            // Act
            var LValidator = new VerifyEmailAddressCommandValidator();
            var LResult = LValidator.Validate(LVerifyEmailAddressCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongEmail_WhenVerifyEmailAddress_ShouldThrowError()
        {
            // Arrange
            var LVerifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = new string('T', 256)
            };

            // Act
            var LValidator = new VerifyEmailAddressCommandValidator();
            var LResult = LValidator.Validate(LVerifyEmailAddressCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }
    }
}
