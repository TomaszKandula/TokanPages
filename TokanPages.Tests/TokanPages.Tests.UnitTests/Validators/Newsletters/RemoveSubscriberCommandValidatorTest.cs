using FluentAssertions;
using TokanPages.Backend.Application.Sender.Newsletters.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Newsletters;

public class RemoveSubscriberCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidId_WhenRemoveSubscriber_ShouldSucceed() 
    {
        // Arrange
        var command = new RemoveNewsletterCommand { Id = Guid.NewGuid() };

        // Act
        var validator = new RemoveNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenRemoveSubscriber_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveNewsletterCommand { Id = Guid.Empty };

        // Act
        var validator = new RemoveNewsletterCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}