using FluentAssertions;
using TokanPages.Backend.Application.Notifications.Mobile.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Notifications.Mobile;

public class SendNotificationCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenSendNotificationCommand_ShouldSucceed()
    {
        // Arrange
        var command = new SendNotificationCommand
        {
            Platform = PlatformType.Aps,
            MessageTitle = DataUtilityService.GetRandomString(),
            MessageBody = DataUtilityService.GetRandomString(),
            Tags = GenerateTags()
        };

        // Act
        var validator = new SendNotificationCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyInput_WhenSendNotificationCommand_ShouldFail()
    {
        // Arrange
        var command = new SendNotificationCommand
        {
            Platform = PlatformType.Aps,
            MessageTitle = string.Empty,
            MessageBody = string.Empty
        };

        // Act
        var validator = new SendNotificationCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public async Task GivenInputTooLong_WhenSendNotificationCommand_ShouldFail()
    {
        // Arrange
        var command = new SendNotificationCommand
        {
            Platform = PlatformType.Aps,
            MessageTitle = DataUtilityService.GetRandomString(500),
            MessageBody = DataUtilityService.GetRandomString(600),
            Tags = GenerateTags(100)
        };

        // Act
        var validator = new SendNotificationCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_225));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_500));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_MANY_TAGS));
    }

    private string[] GenerateTags(int tagsCount = 4)
    {
        var tags = new List<string>(tagsCount);
        for (var index = 0; index < tagsCount; index++)
        {
            tags.Add(DataUtilityService.GetRandomString());
        }

        return tags.ToArray();
    }
}