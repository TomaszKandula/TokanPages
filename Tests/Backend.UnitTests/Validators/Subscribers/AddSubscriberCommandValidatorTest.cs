﻿using Xunit;
using FluentAssertions;
using Backend.TestData;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Validators.Subscribers
{
    public class AddSubscriberCommandValidatorTest
    {
        [Fact]
        public void AddSubscriber_WhenEmailIsGiven_ShouldFinishSuccessful() 
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = DataProvider.GetRandomEmail()
            };

            // Act
            var LValidator = new AddSubscriberCommandValidator();
            var LResult = LValidator.Validate(LAddSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void AddSubscriber_WhenEmailIsEmpty_ShouldFinishSuccessful()
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand
            {
                Email = string.Empty
            };

            // Act
            var LValidator = new AddSubscriberCommandValidator();
            var LResult = LValidator.Validate(LAddSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
