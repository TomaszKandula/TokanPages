using System;
using FluentAssertions;
using TokanPages.Backend.Application.Subscribers.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

public class GetSubscriberQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidId_WhenGetSubscriber_ShouldSucceed() 
    {
        // Arrange
        var query = new GetSubscriberQuery { Id = Guid.NewGuid() };

        // Act
        var validator = new GetSubscriberQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenGetSubscriber_ShouldThrowError()
    {
        // Arrange
        var query = new GetSubscriberQuery { Id = Guid.Empty };

        // Act
        var validator = new GetSubscriberQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}