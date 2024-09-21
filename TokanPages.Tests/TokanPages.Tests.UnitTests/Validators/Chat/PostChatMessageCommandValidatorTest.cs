using FluentAssertions;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Chat;

public class PostChatMessageCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenValidatePostChatMessage_ShouldSucceed() 
    {
        // Arrange
        var command = new PostChatMessageCommand 
        { 
            ChatKey = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new PostChatMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyMessage_WhenValidatePostChatMessage_ShouldSucceed() 
    {
        // Arrange
        var command = new PostChatMessageCommand 
        { 
            ChatKey = DataUtilityService.GetRandomString(),
            Message = string.Empty,
        };

        // Act
        var validator = new PostChatMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenEmptyInput_WhenValidatePostChatMessage_ShouldSucceed() 
    {
        // Arrange
        var command = new PostChatMessageCommand();

        // Act
        var validator = new PostChatMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}