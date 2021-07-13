namespace TokanPages.Backend.Tests.Validators.Mailer
{
    using System.Collections.Generic;
    using Shared.Resources;
    using Shared.Services.DataProviderService;
    using Cqrs.Handlers.Commands.Mailer;
    using FluentAssertions;
    using Xunit;

    public class SendMessageCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public SendMessageCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenSendMessage_ShouldFinishSuccessful() 
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(256),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = string.Empty,
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(256),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = string.Empty,
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = new string('T', 256),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = string.Empty,
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = new string('T', 256),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = string.Empty,
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(256),
                Message = FDataProviderService.GetRandomString()
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string> { FDataProviderService.GetRandomEmail(), FDataProviderService.GetRandomEmail() },
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString(256)
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
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string>(),
                Subject = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString()
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