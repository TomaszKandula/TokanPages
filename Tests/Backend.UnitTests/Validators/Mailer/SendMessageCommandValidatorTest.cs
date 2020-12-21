using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace Backend.UnitTests.Validators.Mailer
{

    public class SendMessageCommandValidatorTest
    {

        [Fact]
        public void SendMessage_WhenAllFieldsAreCorrect_ShouldFinishSuccessful() 
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void SendMessage_WhenFirstNameEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = string.Empty,
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenFirstNameTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = new string('T', 256),
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenLastNameEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = string.Empty,
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenLastNameTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = new string('T', 256),
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenUserEmailEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = string.Empty,
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenUserEmailTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = new string('T', 256),
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenEmailFromEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = string.Empty,
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenEmailFromTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = new string('T', 256),
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenSubjectEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = string.Empty,
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenSubjectTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = new string('T', 256),
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenMessageEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = string.Empty,
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenMessageTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string> { "contact@domain.com", "contact@domain.com" },
                Subject = "Subject",
                Message = new string('T', 256),
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenEmailTosEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = "Ester",
                LastName = "Exposito",
                UserEmail = "ester.exposito@dfds.com",
                EmailFrom = "contact@domain.com",
                EmailTos = new List<string>(),
                Subject = "Subject",
                Message = "Message",
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

    }

}
