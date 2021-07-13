namespace TokanPages.Backend.Tests.Validators.Users
{   
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class AddUserCommandValidatorTest
    {
        private readonly DataUtilityService FDataUtilityService;

        public AddUserCommandValidatorTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenAddUser_ShouldFinishSuccessful() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            { 
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmailAddress_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = string.Empty,
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongEmailAddress_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = new string('T', 256),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyUserAlias_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = string.Empty,
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongUserAlias_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(256),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.USERALIAS_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyFirstName_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = string.Empty,
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongFirstName_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(256),
                LastName = FDataUtilityService.GetRandomString()
            };

            // Act
            var LAddUserCommandValidator = new AddUserCommandValidator();
            var LResults = LAddUserCommandValidator.Validate(LAddUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyLastName_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
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
        public void GivenTooLongLastName_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(256)
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