using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Validators.Subscribers
{

    public class AddSubscriberCommandValidatorTest
    {

        [Fact]
        public void AddSubscriber_WhenEmailIsGiven_ShouldFinishSuccessfull() 
        {

            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = "tokan@dfds.com"
            };

            // Act
            var LValidator = new AddSubscriberCommandValidator();
            var LResult = LValidator.Validate(LAddSubscriberCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void AddSubscriber_WhenEmailIsEmpty_ShouldFinishSuccessfull()
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
