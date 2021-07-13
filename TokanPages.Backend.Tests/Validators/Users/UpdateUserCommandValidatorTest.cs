namespace TokanPages.Backend.Tests.Validators.Users
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Shared.Services.DataProviderService;
    using FluentAssertions;
    using Xunit;

    public class UpdateUserCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateUserCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateUser_ShouldFinishSuccessful()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString()
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
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString()
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
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString()
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = string.Empty,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString()
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(256),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString()
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = string.Empty,
                LastName = FDataProviderService.GetRandomString()
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(256),
                LastName = FDataProviderService.GetRandomString()
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
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
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(256)
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