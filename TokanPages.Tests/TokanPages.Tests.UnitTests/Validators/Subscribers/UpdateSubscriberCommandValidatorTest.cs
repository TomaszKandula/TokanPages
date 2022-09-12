namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Subscribers;

public class UpdateSubscriberCommandValidatorTest : TestBase
{
    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public void GivenValidInputs_WhenUpdateSubscriber_ShouldSucceed(int? count) 
    {
        // Arrange
        var command = new UpdateSubscriberCommand 
        { 
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = count
        };

        // Act
        var validator = new UpdateSubscriberCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenUpdateSubscriber_ShouldThrowError() 
    {
        // Arrange
        var command = new UpdateSubscriberCommand 
        { 
            Id = Guid.Empty,
            Email = string.Empty,
        };

        // Act
        var validator = new UpdateSubscriberCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenUpdateSubscriber_ShouldThrowError() 
    {
        // Arrange
        var command = new UpdateSubscriberCommand 
        { 
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomString(500),
        };

        // Act
        var validator = new UpdateSubscriberCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.EMAIL_TOO_LONG));
    }
}