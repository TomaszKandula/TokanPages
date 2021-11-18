namespace TokanPages.UnitTests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Users;

    public class AuthenticateUserCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenValidFields_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Password = DataUtilityService.GetRandomString()
            };

            // Act
            var authenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var result = authenticateUserCommandValidator.Validate(authenticateUserCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenMissingEmailAddress_WhenAuthenticateUser_ShouldThrowError(string emailAddress)
        {
            // Arrange
            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = DataUtilityService.GetRandomString()
            };

            // Act
            var authenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var result = authenticateUserCommandValidator.Validate(authenticateUserCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenMissingPassword_WhenAuthenticateUser_ShouldThrowError(string password)
        {
            // Arrange
            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Password = password
            };

            // Act
            var authenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var result = authenticateUserCommandValidator.Validate(authenticateUserCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void GivenMissingFields_WhenAuthenticateUser_ShouldThrowError(string emailAddress, string password)
        {
            // Arrange
            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = password
            };

            // Act
            var authenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var result = authenticateUserCommandValidator.Validate(authenticateUserCommand);

            // Assert
            result.Errors.Count.Should().Be(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
        [Fact]
        public void GivenTooLongValues_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = DataUtilityService.GetRandomEmail(300),
                Password = DataUtilityService.GetRandomString(150)
            };

            // Act
            var authenticateUserCommandValidator = new AuthenticateUserCommandValidator();
            var result = authenticateUserCommandValidator.Validate(authenticateUserCommand);

            // Assert
            result.Errors.Count.Should().Be(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
        }
    }
}