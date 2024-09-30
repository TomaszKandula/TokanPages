using FluentAssertions;
using TokanPages.Backend.Application.Content.Assets.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Assets;

public class GetNonVideoAssetQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenGetNonVideoAssetQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetNonVideoAssetQuery
        {
            BlobName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetNonVideoAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInputTooLong_WhenGetNonVideoAssetQuery_ShouldFail()
    {
        // Arrange
        var query = new GetNonVideoAssetQuery
        {
            BlobName = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new GetNonVideoAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.NAME_TOO_LONG));
    }

    [Fact]
    public async Task GivenEmptyInput_WhenGetNonVideoAssetQuery_ShouldFail()
    {
        // Arrange
        var query = new GetNonVideoAssetQuery
        {
            BlobName = string.Empty
        };

        // Act
        var validator = new GetNonVideoAssetQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}