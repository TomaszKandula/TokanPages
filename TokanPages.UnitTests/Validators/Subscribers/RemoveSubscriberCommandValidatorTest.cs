using System;
using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.UnitTests.Validators.Subscribers
{
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
