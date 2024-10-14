using FluentAssertions;
using Moq;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.SpaCachingService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class CachingServiceTest : TestBase
{
    private readonly Mock<IAzureBlobStorageFactory> _mockedFactory = new ();

    public CachingServiceTest()
    {
        var mockedStorage = new Mock<IAzureBlobStorage>();
        mockedStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>())
            )
            .Returns(Task.CompletedTask);

        _mockedFactory
            .Setup(factory => factory.Create(It.IsAny<ILoggerService>()))
            .Returns(mockedStorage.Object);
    }

    [Fact]
    public async Task GivenPdfFile_WhenGeneratePdf_ShouldSucceed()
    {
        // Arrange
        const string url = "https://www.google.com";
        var mockedLogger = new Mock<ILoggerService>();
        var cachingService = new CachingService(mockedLogger.Object, _mockedFactory.Object);

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
        var cachingService = new CachingService(mockedLogger.Object, _mockedFactory.Object);

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
        var cachingService = new CachingService(mockedLogger.Object, _mockedFactory.Object);

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
        var cachingService = new CachingService(mockedLogger.Object, _mockedFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<GeneralException>(() => cachingService.RenderStaticPage(url, pageName));

        result.Message.Should().Be(ErrorCodes.ERROR_UNEXPECTED);
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }
}