using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Assets;

public class AddSingleAssetCommandHandlerTest : TestBase
{
    [Fact]
    public async Task WhenInvokeAddSingleAssetCommand_ShouldSucceed()
    {
        // Arrange
        var command = new AddSingleAssetCommand
        {
            MediaName = DataUtilityService.GetRandomString(),
            MediaType = DataUtilityService.GetRandomString(),
            Data = new byte[4096]
        };

        var databaseContext = GetTestDatabaseContext();
        var loggerService = new Mock<ILoggerService>();
        var storageFactory = new Mock<IAzureBlobStorageFactory>();
        var blobStorage = new Mock<IAzureBlobStorage>();

        blobStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(string.Empty));

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        var handler = new AddSingleAssetCommandHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        blobStorage.Verify(storage => storage.UploadFile(
            It.IsAny<Stream>(), 
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<CancellationToken>()), 
            Times.Once());

        result.Should().NotBeNull();
        result.BlobName.Should().NotBeEmpty();
        result.BlobName.Should().Contain(command.MediaType);
        result.BlobName.Should().Contain(command.MediaName.ToUpper());
    }
}