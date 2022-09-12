﻿namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Subscribers;

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