using FluentAssertions;
using TokanPages.Backend.Application.Chat.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Chat;

public class GetChatDataQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenValidateChatData_ShouldSucceed() 
    {
        // Arrange
        var command = new GetChatDataQuery 
        { 
            ChatKey = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetChatDataQueryValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenInvalidInputs_WhenValidateChatData_ShouldFail() 
    {
        // Arrange
        var command = new GetChatDataQuery 
        { 
            ChatKey = string.Empty
        };

        // Act
        var validator = new GetChatDataQueryValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}