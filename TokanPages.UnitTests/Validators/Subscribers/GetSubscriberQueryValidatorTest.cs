namespace TokanPages.UnitTests.Validators.Subscribers
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Subscribers;

    public class GetSubscriberQueryValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenGetSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var getSubscriberQuery = new GetSubscriberQuery 
            { 
                Id = Guid.NewGuid()
            };

            // Act
            var validator = new GetSubscriberQueryValidator();
            var result = validator.Validate(getSubscriberQuery);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenGetSubscriber_ShouldThrowError()
        {
            // Arrange
            var getSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Empty
            };

            // Act
            var validator = new GetSubscriberQueryValidator();
            var result = validator.Validate(getSubscriberQuery);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}