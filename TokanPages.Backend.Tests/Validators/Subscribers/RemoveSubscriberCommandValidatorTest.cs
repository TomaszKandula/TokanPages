namespace TokanPages.Backend.Tests.Validators.Subscribers
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Subscribers;
    using FluentAssertions;
    using Xunit;

    public class RemoveSubscriberCommandValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenRemoveSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand 
            { 
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new RemoveSubscriberCommandValidator();
            var LResult = LValidator.Validate(LRemoveSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenRemoveSubscriber_ShouldThrowError()
        {
            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new RemoveSubscriberCommandValidator();
            var LResult = LValidator.Validate(LRemoveSubscriberCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}