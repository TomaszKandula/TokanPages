using FluentAssertions;
using TokanPages.Backend.Application.Mailer.Commands;
using TokanPages.Backend.Shared.Resources;
using TokanPages.WebApi.Dto.Mailer.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Mailer;

public class SendNewsletterCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenSendNewsletter_ShouldSucceed() 
    {
        // Arrange
        var command = new SendNewsletterCommand
        {
            Message = "Message",
            Subject = "Subject",
            SubscriberInfo = new List<SubscriberInfo> 
            { 
                new() 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenSendNewsletter_ShouldThrowError()
    {
        // Arrange
        var command = new SendNewsletterCommand
        {
            Message = string.Empty,
            Subject = string.Empty,
            SubscriberInfo = new List<SubscriberInfo>()
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenSendNewsletter_ShouldThrowError()
    {
        // Arrange
        var command = new SendNewsletterCommand
        {
            Message = DataUtilityService.GetRandomString(500),
            Subject = DataUtilityService.GetRandomString(500),
            SubscriberInfo = new List<SubscriberInfo> 
            { 
                new() 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));
    }
}