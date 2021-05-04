using Xunit;
using FluentAssertions;
using System;
using Backend.DataProviders;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Validators.Users
{
    public class UpdateUserCommandValidatorTest
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateUser_ShouldFinishSuccessful()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmailAddress_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = string.Empty,
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongEmailAddress_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = new string('T', 256),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyUserAlias_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = string.Empty,
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongUserAlias_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(256),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyFirstName_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = string.Empty,
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongFirstName_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(256),
                LastName = StringProvider.GetRandomString()
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyLastName_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = string.Empty
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongLastName_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(256)
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        }
    }
}
