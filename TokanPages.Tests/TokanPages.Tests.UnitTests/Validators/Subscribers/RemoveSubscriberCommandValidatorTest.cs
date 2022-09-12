using System;
using FluentAssertions;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

public class RemoveSubscriberCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidId_WhenRemoveSubscriber_ShouldSucceed() 
    {
        // Arrange
        var command = new RemoveSubscriberCommand { Id = Guid.NewGuid() };

        // Act
        var validator = new RemoveSubscriberCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenRemoveSubscriber_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveSubscriberCommand { Id = Guid.Empty };

        // Act
        var validator = new RemoveSubscriberCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}