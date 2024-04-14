using FluentAssertions;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Subscribers;

public class AddSubscriberCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenEmail_WhenAddSubscriber_ShouldSucceed() 
    {
        // Arrange
        var command = new AddNewsletterCommand { Email = DataUtilityService.GetRandomEmail() };

        // Act
        var validator = new AddNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyEmail_WhenAddSubscriber_ShouldThrowError()
    {
        // Arrange
        var command = new AddNewsletterCommand { Email = string.Empty };

        // Act
        var validator = new AddNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}