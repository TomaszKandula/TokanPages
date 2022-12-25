using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Articles;
using TokanPages.Tests.EndToEndTests.Helpers;
using Xunit;

namespace TokanPages.Tests.EndToEndTests.Controllers;

public class AssetsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _factory;

    public AssetsControllerTest(CustomWebApplicationFactory<TestStartup> factory) => _factory = factory;

    [Theory]
    [InlineData("images/icons/__github.png")]
    [InlineData("images/icons/__linkedin.png")]
    public async Task GivenValidBlobName_WhenRequestingAsset_ShouldSucceed(string blobName)
    {
        // Arrange
        var uri = $"{BaseUriAssets}/getAsset/?BlobName={blobName}";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenInvalidBlobName_WhenRequestingAsset_ShouldThrowError()
    {
        // Arrange
        var uri = $"{BaseUriAssets}/getAsset/?BlobName={DataUtilityService.GetRandomString(useAlphabetOnly: true)}";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetArticleAsset_ShouldSucceed()
    {
        // Arrange
        var userId = Article1.Id;
        var uri = $"{BaseUriAssets}/getArticleAsset/?Id={userId}&assetName=image.jpg";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticleAsset_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var uri = $"{BaseUriAssets}/getArticleAsset/?Id={userId}&assetName=image.jpg";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }
}