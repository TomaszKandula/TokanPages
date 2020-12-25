using Xunit;
using FluentAssertions;
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
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
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
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
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Kandula"
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = string.Empty,
                FirstName = "Tomasz",
                LastName = "Kandula"
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = new string('T', 256),
                FirstName = "Tomasz",
                LastName = "Kandula"
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = string.Empty,
                LastName = "Kandula"
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = new string('T', 256),
                LastName = "Kandula"
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
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
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = new string('T', 256)
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
