namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Subscribers;

public class RemoveSubscriberCommandValidatorTest
{
    [Fact]
    public void GivenCorrectId_WhenRemoveSubscriber_ShouldFinishSuccessful() 
    {
        // Arrange
        var removeSubscriberCommand = new RemoveSubscriberCommand 
        { 
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new RemoveSubscriberCommandValidator();
        var result = validator.Validate(removeSubscriberCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenRemoveSubscriber_ShouldThrowError()
    {
        // Arrange
        var removeSubscriberCommand = new RemoveSubscriberCommand
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new RemoveSubscriberCommandValidator();
        var result = validator.Validate(removeSubscriberCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}