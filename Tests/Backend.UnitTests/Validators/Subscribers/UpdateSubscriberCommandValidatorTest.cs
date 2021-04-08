using System;
using Xunit;
using FluentAssertions;
using Backend.TestData;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Validators.Subscribers
{
    public class UpdateSubscriberCommandValidatorTest
    {
        [Fact]
        public void UpdateSubscriber_WhenAllFieldsAreCorrect_ShouldFinishSuccessful() 
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand 
            { 
                Id = Guid.NewGuid(),
                Email = DataProvider.GetRandomEmail(),
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
        public void UpdateSubscriber_WhenAllFieldsAreCorrectAndCountIsNull_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = DataProvider.GetRandomEmail(),
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
        public void UpdateSubscriber_WhenIdIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Empty,
                Email = DataProvider.GetRandomEmail(),
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
        public void UpdateSubscriber_WhenEmailIsEmpty_ShouldThrowError()
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
        public void UpdateSubscriber_WhenEmailTooLong_ShouldThrowError()
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
        public void UpdateSubscriber_WhenCountIsLessThanZero_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = DataProvider.GetRandomEmail(),
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
