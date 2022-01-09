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
    private const string ApiBaseUrl = "/api/v1.0/assets";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public AssetsControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Theory]
    [InlineData("images/icons/__github.png")]
    [InlineData("images/icons/__linkedin.png")]
    public async Task GivenValidBlobName_WhenRequestingAsset_ShouldSucceed(string blobName)
    {
        // Arrange
        var request = $"{ApiBaseUrl}/getAsset/?BlobName={blobName}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenInvalidBlobName_WhenRequestingAsset_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/getAsset/?BlobName={DataUtilityService.GetRandomString(useAlphabetOnly: true)}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetArticleAsset_ShouldSucceed()
    {
        // Arrange
        var testUserId = Article1.Id;
        var request = $"{ApiBaseUrl}/getArticleAsset/?Id={testUserId}&assetName=image.jpg&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticleAsset_ShouldThrowError()
    {
        // Arrange
        var testUserId = Guid.NewGuid();
        var request = $"{ApiBaseUrl}/getArticleAsset/?Id={testUserId}&assetName=image.jpg&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }
}