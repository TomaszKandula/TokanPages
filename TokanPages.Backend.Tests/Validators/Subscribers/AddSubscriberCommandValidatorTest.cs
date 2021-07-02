using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services.DataProviderService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.Backend.Tests.Validators.Subscribers
{
    public class AddSubscriberCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public AddSubscriberCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenEmail_WhenAddSubscriber_ShouldFinishSuccessful() 
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = FDataProviderService.GetRandomEmail()
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
