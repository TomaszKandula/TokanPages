using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Resources;
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
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

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
        var mockedDateTime = new Mock<IDateTimeService>();
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
            It.IsAny<DateTime>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()            
            ))
            .ReturnsAsync(true);

        var handler = new UpdateArticleContentCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTime.Object, 
            mockedAzureBlobStorageFactory.Object,
            mockedArticlesRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenNonExistingArticleAndActiveUser_WhenUpdateArticleContent_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var articleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var command = new UpdateArticleContentCommand
        {
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var mockedUserService = new Mock<IUserService>();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>(); 
        var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(repository => repository.UpdateArticleContent(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<DateTime>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()            
            ))
            .ReturnsAsync(false);

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

        var handler = new UpdateArticleContentCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTime.Object, 
            mockedAzureBlobStorageFactory.Object,
            mockedArticlesRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}