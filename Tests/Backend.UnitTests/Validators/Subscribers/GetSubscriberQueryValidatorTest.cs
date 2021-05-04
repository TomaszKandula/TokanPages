using System;
using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace Backend.UnitTests.Validators.Subscribers
{
    public class GetSubscriberQueryValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenGetSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LGetSubscriberQuery = new GetSubscriberQuery 
            { 
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new GetSubscriberQueryValidator();
            var LResult = LValidator.Validate(LGetSubscriberQuery);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenGetSubscriber_ShouldThrowError()
        {
            // Arrange
            var LGetSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new GetSubscriberQueryValidator();
            var LResult = LValidator.Validate(LGetSubscriberQuery);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
