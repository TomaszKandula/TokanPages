using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using Moq;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.Services.SpaCachingService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class CachingServiceTest : TestBase
{
    private readonly Mock<IAzureBlobStorageFactory> _mockedStorageFactory = new ();

    private readonly Mock<IHttpClientServiceFactory> _mockedHttpFactory = new();
    
    public CachingServiceTest()
    {
        var mockedStorage = new Mock<IAzureBlobStorage>();
        var mockedHttp = new Mock<IHttpClientService>();
        var testContent = new ExecutionResult
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/html"),
            Content = new byte[1024]
        };

        mockedStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>())
            )
            .Returns(Task.CompletedTask);

        mockedHttp
            .Setup(service => service.Execute(
                It.IsAny<Configuration>(), 
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(testContent));

        _mockedStorageFactory
            .Setup(factory => factory.Create(
                It.IsAny<ILoggerService>()))
            .Returns(mockedStorage.Object);

        _mockedHttpFactory
            .Setup(factory => factory.Create(
                It.IsAny<bool>(), 
                It.IsAny<ILoggerService>()))
            .Returns(mockedHttp.Object);
    }

    [Fact]
    public async Task GivenPdfFile_WhenGeneratePdf_ShouldSucceed()
    {
        // Arrange
        const string url = "https://www.google.com";
        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        var result = await cachingService.GeneratePdf(url);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().Contain(".pdf");
    }

    [Fact]
    public async Task GivenPageUrlAndName_WhenRenderStaticPage_ShouldSucceed()
    {
        // Arrange
        const string url = "https://www.google.com";
        const string pageName = "google";
        const string expectedFileName = $"{pageName}.html";

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        var result = await cachingService.RenderStaticPage(url, pageName);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().Contain(expectedFileName);
    }

    [Fact]
    public async Task GivenWrongUrl_WhenGeneratePdf_ShouldThrowError()
    {
        // Arrange
        const string url = "wrong url. com";

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<GeneralException>(() => cachingService.GeneratePdf(url));

        result.Message.Should().Be(ErrorCodes.ERROR_UNEXPECTED);
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    [Fact]
    public async Task GivenWrongUrl_WhenRenderStaticPage_ShouldThrowError()
    {
        // Arrange
        const string url = "wrong url. com";
        const string pageName = "test";

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<GeneralException>(() => cachingService.RenderStaticPage(url, pageName));

        result.Message.Should().Be(ErrorCodes.ERROR_UNEXPECTED);
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    [Fact]
    public async Task GivenFilesAndBaseUrl_WhenSaveStaticFiles_ShouldSucceed()
    {
        // Arrange
        const string baseUrl = "https://test.com";
        var files = new[]
        {
            "main.05734.js",
            "aos.css",
            "robots.txt"
        };

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        var result = await cachingService.SaveStaticFiles(files, baseUrl);

        // Assert
        result.Should().Be(files.Length);
    }

    [Fact]
    public async Task GivenNoFilesAndBaseUrl_WhenSaveStaticFiles_ShouldNotProcessed()
    {
        // Arrange
        const string baseUrl = "https://test.com";
        var files = Array.Empty<string>();

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        var result = await cachingService.SaveStaticFiles(files, baseUrl);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async Task GivenFilesAndNoBaseUrl_WhenSaveStaticFiles_ShouldNotProcessed()
    {
        // Arrange
        const string baseUrl = "";
        var files = new[]
        {
            "main.05734.js",
            "aos.css",
            "robots.txt"
        };

        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(
            mockedLogger.Object, 
            _mockedStorageFactory.Object, 
            _mockedHttpFactory.Object);

        // Act
        var result = await cachingService.SaveStaticFiles(files, baseUrl);

        // Assert
        result.Should().Be(0);
    }
}