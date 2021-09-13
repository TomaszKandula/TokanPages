namespace TokanPages.Backend.Tests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;

    public class UpdateUserPasswordCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenUserIdAndNewPassword_WhenUpdateUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString()
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenNoUserIdAndNewPassword_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.Empty,
                NewPassword = DataUtilityService.GetRandomString()
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenUserIdAndNoNewPassword_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.NewGuid(),
                NewPassword = string.Empty
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenNoUserIdAndNoNewPassword_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.Empty,
                NewPassword = string.Empty
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(2);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResults.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenUserIdAndTooLongNewPassword_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString(500)
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
        }

        [Fact]
        public void GivenNoUserIdAndTooLongNewPassword_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.Empty,
                NewPassword = DataUtilityService.GetRandomString(500)
            };

            // Act
            var LUpdateUserPasswordCommandValidator = new UpdateUserPasswordCommandValidator();
            var LResults = LUpdateUserPasswordCommandValidator.Validate(LUpdateUserPasswordCommand);

            // Assert
            LResults.Errors.Count.Should().Be(2);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResults.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.PASSWORD_TOO_LONG));
        }
    }
}