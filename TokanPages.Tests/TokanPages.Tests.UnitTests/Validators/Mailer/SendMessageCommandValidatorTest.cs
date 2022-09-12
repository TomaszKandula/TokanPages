using System.Collections.Generic;
using FluentAssertions;
using TokanPages.Backend.Application.Mailer.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Mailer;

public class SendMessageCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenSendMessage_ShouldSucceed() 
    {
        // Arrange
        var command = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenSendMessage_ShouldThrowError() 
    {
        // Arrange
        var command = new SendMessageCommand
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserEmail = string.Empty,
            EmailFrom = string.Empty,
            EmailTos = new List<string>(),
            Subject = string.Empty,
            Message = string.Empty,
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(7);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[6].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongString_WhenSendMessage_ShouldThrowError() 
    {
        // Arrange
        var command = new SendMessageCommand
        {
            FirstName = DataUtilityService.GetRandomString(500),
            LastName = DataUtilityService.GetRandomString(500),
            UserEmail = DataUtilityService.GetRandomEmail(500),
            EmailFrom = DataUtilityService.GetRandomEmail(500),
            EmailTos = new List<string> { DataUtilityService.GetRandomEmail() },
            Subject = DataUtilityService.GetRandomString(500),
            Message = DataUtilityService.GetRandomString(10100)
        };

        // Act
        var validator = new SendMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(6);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.FIRST_NAME_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LAST_NAME_TOO_LONG));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));
    }
}