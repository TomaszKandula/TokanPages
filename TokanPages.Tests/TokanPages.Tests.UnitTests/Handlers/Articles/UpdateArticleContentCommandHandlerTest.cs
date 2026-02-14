using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class UpdateArticleContentCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenExistingArticleAndActiveUser_WhenUpdateArticleContent_ShouldUpdateEntity() 
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();
        var command = new UpdateArticleContentCommand 
        { 
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var mockedUserService = new Mock<IUserService>();
        var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedAzureBlobStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(string.Empty));

        mockedAzureBlobStorageFactory
            .Setup(factory => factory.Create(mockedLogger.Object))
            .Returns(mockedAzureBlobStorage.Object);

        mockedArticlesRepository
            .Setup(repository => repository.UpdateArticleContent(
            It.IsAny<Guid>(), 
            It.IsAny<Guid>(), 
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()            
            ))
            .Returns(Task.CompletedTask);

        var handler = new UpdateArticleContentCommandHandler(
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedAzureBlobStorageFactory.Object,
            mockedArticlesRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }
}