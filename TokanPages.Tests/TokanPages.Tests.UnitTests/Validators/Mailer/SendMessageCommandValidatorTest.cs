namespace TokanPages.Tests.UnitTests.Validators.Mailer;

using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Mailer;

public class SendMessageCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenAllFieldsAreCorrect_WhenSendMessage_ShouldFinishSuccessful() 
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenFirstNameEmpty_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = string.Empty,
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenFirstNameTooLong_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(256),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
    }

    [Fact]
    public void GivenLastNameEmpty_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = string.Empty,
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenLastNameTooLong_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(256),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyUserEmail_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = string.Empty,
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenUserEmailTooLong_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = new string('T', 256),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyEmailFrom_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = string.Empty,
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongEmailFrom_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = new string('T', 256),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
    }

    [Fact]
    public void GivenEmptySubject_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = string.Empty,
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongSubject_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(256),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyMessage_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = string.Empty,
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var validate = validator.Validate(sendMessageCommand);

        // Assert
        validate.Errors.Count.Should().Be(1);
        validate.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongMessage_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail(), DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString(10001)
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyEmailTos_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string>(),
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(sendMessageCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}