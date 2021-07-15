namespace TokanPages.Backend.Tests.Validators.Subscribers
{
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Subscribers;
    using FluentAssertions;
    using Xunit;

    public class AddSubscriberCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenEmail_WhenAddSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = DataUtilityService.GetRandomEmail()
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