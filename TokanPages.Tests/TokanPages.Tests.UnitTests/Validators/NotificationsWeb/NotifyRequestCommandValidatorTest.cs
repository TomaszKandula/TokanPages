using FluentAssertions;
using TokanPages.Backend.Application.Notifications.Web.Command;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.NotificationsWeb;

public class NotifyRequestCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenHandlerName_WhenInvokeNotifyCommand_ShouldSucceed() 
    {
        // Arrange
        var command = new NotifyRequestCommand
        {
            Handler = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new NotifyRequestCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenMissingHandlerName_WhenInvokeNotifyCommand_ShouldThrowError() 
    {
        // Arrange
        var command = new NotifyRequestCommand();

        // Act
        var validator = new NotifyRequestCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenHandlerNameTooLong_WhenInvokeNotifyCommand_ShouldThrowError() 
    {
        // Arrange
        var command = new NotifyRequestCommand
        {
            Handler = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new NotifyRequestCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.NAME_TOO_LONG));
    }
}