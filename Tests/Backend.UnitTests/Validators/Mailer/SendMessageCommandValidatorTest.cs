using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Backend.TestData;
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(256),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = string.Empty,
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(256),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = string.Empty,
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = new string('T', 256),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = string.Empty,
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = new string('T', 256),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = string.Empty,
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(256),
                Message = DataProvider.GetRandomString()
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { DataProvider.GetRandomEmail(), DataProvider.GetRandomEmail() },
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString(256)
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
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string>(),
                Subject = DataProvider.GetRandomString(),
                Message = DataProvider.GetRandomString()
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
