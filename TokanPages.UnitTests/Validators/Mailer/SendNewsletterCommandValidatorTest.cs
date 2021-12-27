namespace TokanPages.UnitTests.Validators.Mailer;

using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Backend.Shared.Models;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Mailer;

public class SendNewsletterCommandValidatorTest
{
    [Fact]
    public void GivenAllFieldsAreCorrect_WhenSendNewsletter_ShouldFinishSuccessful() 
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = "Message",
            Subject = "Subject",
            SubscriberInfo = new List<SubscriberInfo> 
            { 
                new () 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenSubscriberInfoIsEmpty_WhenSendNewsletter_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = "Message",
            Subject = "Subject",
            SubscriberInfo = new List<SubscriberInfo>()
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenEmptySubject_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = "Message",
            Subject = string.Empty,
            SubscriberInfo = new List<SubscriberInfo>
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongSubject_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = "Message",
            Subject = new string('T', 256),
            SubscriberInfo = new List<SubscriberInfo>
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));
    }

    [Fact]
    public void GivenEmptyMessage_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = string.Empty,
            Subject = "Subject",
            SubscriberInfo = new List<SubscriberInfo>
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongMessage_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = new string('T', 256),
            Subject = "Subject",
            SubscriberInfo = new List<SubscriberInfo>
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "tokan@dfds.com"
                }
            }
        };

        // Act
        var validator = new SendNewsletterCommandValidator();
        var result = validator.Validate(sendNewsletterCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));
    }
}