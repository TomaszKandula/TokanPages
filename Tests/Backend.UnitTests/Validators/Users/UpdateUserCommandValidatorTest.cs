using Xunit;
using FluentAssertions;
using System;
using Backend.TestData;
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
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
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
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
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = string.Empty,
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(256),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = string.Empty,
                LastName = DataProvider.GetRandomString()
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(256),
                LastName = DataProvider.GetRandomString()
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
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
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(256)
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
