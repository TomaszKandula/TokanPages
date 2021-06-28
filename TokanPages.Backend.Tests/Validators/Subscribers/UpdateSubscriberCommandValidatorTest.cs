using System;
using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.DataProviderService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.Backend.Tests.Validators.Subscribers
{
    public class UpdateSubscriberCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateSubscriberCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand 
            { 
                Id = Guid.NewGuid(),
                Email = FDataProviderService.GetRandomEmail(),
                IsActivated = true,
                Count = 0
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenAllFieldsAreCorrectAndCountIsNull_WhenUpdateSubscriber_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = FDataProviderService.GetRandomEmail(),
                IsActivated = true,
                Count = null
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Empty,
                Email = FDataProviderService.GetRandomEmail(),
                IsActivated = true,
                Count = 0
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyEmail_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = string.Empty,
                IsActivated = true,
                Count = 0
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmailTooLong_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = new string('T', 256),
                IsActivated = true,
                Count = 0
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenCountIsLessThanZero_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = FDataProviderService.GetRandomEmail(),
                IsActivated = true,
                Count = -1
            };

            // Act
            var LValidator = new UpdateSubscriberCommandValidator();
            var LResult = LValidator.Validate(LUpdateSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
    }
}
