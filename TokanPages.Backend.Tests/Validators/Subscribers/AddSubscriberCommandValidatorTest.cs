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
            var addSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = DataUtilityService.GetRandomEmail()
            };

            // Act
            var validator = new AddSubscriberCommandValidator();
            var result = validator.Validate(addSubscriberCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyEmail_WhenAddSubscriber_ShouldFinishSuccessful()
        {
            // Arrange
            var addSubscriberCommand = new AddSubscriberCommand
            {
                Email = string.Empty
            };

            // Act
            var validator = new AddSubscriberCommandValidator();
            var result = validator.Validate(addSubscriberCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}