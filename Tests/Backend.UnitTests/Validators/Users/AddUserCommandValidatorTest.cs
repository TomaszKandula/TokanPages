using Xunit;
using FluentAssertions;
using Backend.TestData;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Validators.Users
{   
    public class AddUserCommandValidatorTest
    {
        [Fact]
        public void AddUser_WhenAllFieldsAreCorrect_ShouldFinishSuccessfull() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            { 
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void AddUser_WhenEmailAddressIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = string.Empty,
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void AddUser_WhenEmailAddressIsTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = new string('T', 256),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void AddUser_WhenUserAliasIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = string.Empty,
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void AddUser_WhenUserAliasIsTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(256),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));
        }

        [Fact]
        public void AddUser_WhenFirstNameIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = string.Empty,
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void AddUser_WhenFirstNameIsTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(256),
                LastName = DataProvider.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        }

        [Fact]
        public void AddUser_WhenLastNameIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = string.Empty
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void AddUser_WhenLastNameIsTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(256)
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        }
    }
}
