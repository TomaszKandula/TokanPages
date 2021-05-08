using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using TokanPages.DataProviders;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace TokanPages.UnitTests.Validators.Mailer
{
    public class SendMessageCommandValidatorTest
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenSendMessage_ShouldFinishSuccessful() 
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenFirstNameEmpty_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = string.Empty,
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenFirstNameTooLong_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(256),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        }

        [Fact]
        public void GivenLastNameEmpty_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = string.Empty,
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenLastNameTooLong_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(256),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyUserEmail_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = string.Empty,
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenUserEmailTooLong_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = new string('T', 256),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyEmailFrom_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = string.Empty,
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongEmailFrom_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = new string('T', 256),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenEmptySubject_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = string.Empty,
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongSubject_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(256),
                Message = StringProvider.GetRandomString()
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyMessage_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
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
        public void GivenTooLongMessage_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { StringProvider.GetRandomEmail(), StringProvider.GetRandomEmail() },
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString(256)
            };

            // Act
            var LValidator = new SendMessageCommandValidator();
            var LResult = LValidator.Validate(LSendMessageCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyEmailTos_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string>(),
                Subject = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString()
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
