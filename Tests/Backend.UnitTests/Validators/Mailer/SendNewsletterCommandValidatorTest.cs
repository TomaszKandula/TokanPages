using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace Backend.UnitTests.Validators.Mailer
{

    public class SendNewsletterCommandValidatorTest
    {

        [Fact]
        public void SendNewsletter_WhenAllFieldsAreCorrect_ShouldFinishSuccessfull() 
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = "Message",
                Subject = "Subject",
                SubscriberInfo = new List<SubscriberInfo> 
                { 
                    new SubscriberInfo 
                    { 
                        Id = Guid.NewGuid().ToString(),
                        Email = "tokan@dfds.com"
                    }
                }

            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void SendNewsletter_WhenSubscriberInfoIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = "Message",
                Subject = "Subject",
                SubscriberInfo = new List<SubscriberInfo>()
            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenSubjectEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = "Message",
                Subject = string.Empty,
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new SubscriberInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "tokan@dfds.com"
                    }
                }

            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenSubjectTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = "Message",
                Subject = new string('T', 256),
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new SubscriberInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "tokan@dfds.com"
                    }
                }

            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.SUBJECT_TOO_LONG));

        }

        [Fact]
        public void SendMessage_WhenMessageEmpty_ShouldThrowError()
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = string.Empty,
                Subject = "Subject",
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new SubscriberInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "tokan@dfds.com"
                    }
                }

            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void SendMessage_WhenMessageTooLong_ShouldThrowError()
        {

            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = new string('T', 256),
                Subject = "Subject",
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new SubscriberInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "tokan@dfds.com"
                    }
                }

            };

            // Act
            var LValidator = new SendNewsletterCommandValidator();
            var LResult = LValidator.Validate(LSendNewsletterCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.MESSAGE_TOO_LONG));

        }

    }

}
