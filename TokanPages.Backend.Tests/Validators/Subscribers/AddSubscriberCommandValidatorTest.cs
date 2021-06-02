using Xunit;
using FluentAssertions;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.Backend.Tests.Validators.Subscribers
{
    public class AddSubscriberCommandValidatorTest
    {
        [Fact]
        public void GivenEmail_WhenAddSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = StringProvider.GetRandomEmail()
            };

            // Act
            var LValidator = new AddSubscriberCommandValidator();
            var LResult = LValidator.Validate(LAddSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmail_WhenAddSubscriber_ShouldFinishSuccessful()
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
