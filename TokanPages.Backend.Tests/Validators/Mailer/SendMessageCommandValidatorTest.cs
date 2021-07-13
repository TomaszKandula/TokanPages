namespace TokanPages.Backend.Tests.Validators.Mailer
{
    using System.Collections.Generic;
    using Shared.Resources;
    using Shared.Services.DataUtilityService;
    using Cqrs.Handlers.Commands.Mailer;
    using FluentAssertions;
    using Xunit;

    public class SendMessageCommandValidatorTest
    {
        private readonly DataUtilityService FDataUtilityService;

        public SendMessageCommandValidatorTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenSendMessage_ShouldFinishSuccessful() 
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(256),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = string.Empty,
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(256),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = string.Empty,
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = new string('T', 256),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = string.Empty,
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = new string('T', 256),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = string.Empty,
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(256),
                Message = FDataUtilityService.GetRandomString()
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string> { FDataUtilityService.GetRandomEmail(), FDataUtilityService.GetRandomEmail() },
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString(256)
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                UserEmail = FDataUtilityService.GetRandomEmail(),
                EmailFrom = FDataUtilityService.GetRandomEmail(),
                EmailTos = new List<string>(),
                Subject = FDataUtilityService.GetRandomString(),
                Message = FDataUtilityService.GetRandomString()
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