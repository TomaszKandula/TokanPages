namespace TokanPages.Backend.Tests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;

    public class AuthenticateUserCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenValidFields_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Password = DataUtilityService.GetRandomString()
            };

            // Act
            var LAuthenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var LResult = LAuthenticateUserCommandValidator.Validate(LAuthenticateUserCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenMissingEmailAddress_WhenAuthenticateUser_ShouldThrowError(string AEmailAddress)
        {
            // Arrange
            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = AEmailAddress,
                Password = DataUtilityService.GetRandomString()
            };

            // Act
            var LAuthenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var LResult = LAuthenticateUserCommandValidator.Validate(LAuthenticateUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenMissingPassword_WhenAuthenticateUser_ShouldThrowError(string APassword)
        {
            // Arrange
            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Password = APassword
            };

            // Act
            var LAuthenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var LResult = LAuthenticateUserCommandValidator.Validate(LAuthenticateUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void GivenMissingFields_WhenAuthenticateUser_ShouldThrowError(string AEmailAddress, string APassword)
        {
            // Arrange
            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = AEmailAddress,
                Password = APassword
            };

            // Act
            var LAuthenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var LResult = LAuthenticateUserCommandValidator.Validate(LAuthenticateUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Fact]
        public void GivenTooLongValues_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(300),
                Password = DataUtilityService.GetRandomString(150)
            };

            // Act
            var LAuthenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var LResult = LAuthenticateUserCommandValidator.Validate(LAuthenticateUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
        }
    }
}