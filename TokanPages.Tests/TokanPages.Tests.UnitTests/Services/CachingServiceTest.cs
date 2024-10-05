using FluentAssertions;
using TokanPages.Services.SpaCachingService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class CachingServiceTest : TestBase
{
    [Fact]
    public async Task GivenPdfFile_WhenGeneratePdf_ShouldSucceed()
    {
        // Arrange
        const string url = "http://www.google.com";
        var cachingService = new CachingService();

        // Act
        var result = await cachingService.GeneratePdf(url);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenPageUrlAndName_WhenRenderStaticPage_ShouldSucceed()
    {
        // Arrange
        const string url = "http://www.google.com";
        const string pageName = "google";
        var cachingService = new CachingService();

        // Act
        var result = await cachingService.RenderStaticPage(url, pageName);

        // Assert
        result.Should().NotBeEmpty();
    }
}