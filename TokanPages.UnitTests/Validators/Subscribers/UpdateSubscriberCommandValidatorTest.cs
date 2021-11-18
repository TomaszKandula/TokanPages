namespace TokanPages.UnitTests.Validators.Subscribers
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Subscribers;

    public class UpdateSubscriberCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand 
            { 
                Id = Guid.NewGuid(),
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 0
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenAllFieldsAreCorrectAndCountIsNull_WhenUpdateSubscriber_ShouldFinishSuccessful()
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = null
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Empty,
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 0
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyEmail_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = string.Empty,
                IsActivated = true,
                Count = 0
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmailTooLong_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = new string('T', 256),
                IsActivated = true,
                Count = 0
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
        }

        [Fact]
        public void GivenCountIsLessThanZero_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var updateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.NewGuid(),
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = -1
            };

            // Act
            var validator = new UpdateSubscriberCommandValidator();
            var result = validator.Validate(updateSubscriberCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
    }
}