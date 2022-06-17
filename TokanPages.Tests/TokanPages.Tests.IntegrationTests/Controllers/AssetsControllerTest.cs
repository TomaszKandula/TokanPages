namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared.Resources;
using Backend.Database.Initializer.Data.Articles;
using Factories;

public class AssetsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public AssetsControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Theory]
    [InlineData("images/icons/__github.png")]
    [InlineData("images/icons/__linkedin.png")]
    public async Task GivenValidBlobName_WhenRequestingAsset_ShouldSucceed(string blobName)
    {
        // Arrange
        var uri = $"{BaseUriAssets}/getAsset/?BlobName={blobName}&noCache=true";
        var httpClient = _webApplicationFactory
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
        var uri = $"{BaseUriAssets}/getAsset/?BlobName={DataUtilityService.GetRandomString(useAlphabetOnly: true)}&noCache=true";
        var httpClient = _webApplicationFactory
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
        var uri = $"{BaseUriAssets}/getArticleAsset/?Id={userId}&assetName=image.jpg&noCache=true";
        var httpClient = _webApplicationFactory
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
        var uri = $"{BaseUriAssets}/getArticleAsset/?Id={userId}&assetName=image.jpg&noCache=true";
        var httpClient = _webApplicationFactory
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