using FluentAssertions;
using TokanPages.Backend.Application.Content.Assets.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Assets;

public class GetArticleAssetQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenGetArticleAssetQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetArticleAssetQuery
        {
            Id = DataUtilityService.GetRandomString(),
            AssetName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetArticleAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInputTooLong_WhenGetArticleAssetQuery_ShouldFail()
    {
        // Arrange
        var query = new GetArticleAssetQuery
        {
            Id = DataUtilityService.GetRandomString(),
            AssetName = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new GetArticleAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.NAME_TOO_LONG));
    }

    [Fact]
    public async Task GivenEmptyInput_WhenGetArticleAssetQuery_ShouldFail()
    {
        // Arrange
        var query = new GetArticleAssetQuery
        {
            Id = string.Empty,
            AssetName = string.Empty
        };

        // Act
        var validator = new GetArticleAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}