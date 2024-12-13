using FluentAssertions;
using TokanPages.Backend.Application.Content.Cached.Commands;
using TokanPages.Backend.Application.Content.Cached.Commands.Models;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Cached;

public class OrderSpaCachingCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInput_WhenOrderSpaCaching_ShouldSucceed()
    {
        // Arrange
        var command = new OrderSpaCachingCommand
        {
            GetUrl = DataUtilityService.GetRandomString(),
            PostUrl = DataUtilityService.GetRandomString(),
            Files = new [] { "test.txt", "test.jpg" },
            Paths = new List<RoutePath>
            {
                new()
                {
                    Name = DataUtilityService.GetRandomString(),
                    Url = DataUtilityService.GetRandomString()
                },
                new()
                {
                    Name = DataUtilityService.GetRandomString(),
                    Url = DataUtilityService.GetRandomString()
                }
            }
        };

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenUrlsWithoutLists_WhenOrderSpaCaching_ShouldSucceed()
    {
        // Arrange
        var command = new OrderSpaCachingCommand
        {
            GetUrl = DataUtilityService.GetRandomString(),
            PostUrl = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void GivenMissingGetUrl_WhenOrderSpaCaching_ShouldFail()
    {
        // Arrange
        var command = new OrderSpaCachingCommand
        {
            PostUrl = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenMissingPostUrl_WhenOrderSpaCaching_ShouldFail()
    {
        // Arrange
        var command = new OrderSpaCachingCommand
        {
            GetUrl = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenMissingUrls_WhenOrderSpaCaching_ShouldFail()
    {
        // Arrange
        var command = new OrderSpaCachingCommand();

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenEmptyUrls_WhenOrderSpaCaching_ShouldFail()
    {
        // Arrange
        var command = new OrderSpaCachingCommand
        {
            GetUrl = string.Empty,
            PostUrl = string.Empty
        };

        // Act
        var validator = new OrderSpaCachingCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}