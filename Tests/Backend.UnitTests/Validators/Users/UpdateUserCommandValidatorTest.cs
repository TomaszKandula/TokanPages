using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Validators.Users
{

    public class UpdateUserCommandValidatorTest
    {

        [Fact]
        public void UpdateUser_WhenAllFieldsAreCorrect_ShouldFinishSuccessfull()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();

        }

        [Fact]
        public void UpdateUser_WhenEmailAddressIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = string.Empty,
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateUser_WhenEmailAddressIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = new string('T', 256),
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));

        }

        [Fact]
        public void UpdateUser_WhenUserAliasIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = string.Empty,
                FirstName = "Tomasz",
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateUser_WhenUserAliasIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = new string('T', 256),
                FirstName = "Tomasz",
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));

        }

        [Fact]
        public void UpdateUser_WhenFirstNameIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = string.Empty,
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateUser_WhenFirstNameIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = new string('T', 256),
                LastName = "Kandula"
            };

            // Act
            var LUpdateUserCommandValidator = new UpdateUserCommandValidator();
            var LResults = LUpdateUserCommandValidator.Validate(LUpdateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));

        }

        [Fact]
        public void UpdateUser_WhenLastNameIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
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
        public void UpdateUser_WhenLastNameIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = new string('T', 256)
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
