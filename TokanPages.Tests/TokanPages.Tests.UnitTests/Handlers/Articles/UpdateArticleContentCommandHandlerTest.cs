using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
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

        var users = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Article.Articles
        {
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = userId
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

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
            mockedAzureBlobStorageFactory.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles.FindAsync(command.Id);
        articlesEntity.Should().NotBeNull();
        articlesEntity?.Title.Should().Be(command.Title);
        articlesEntity?.Description.Should().Be(command.Description); 
        articlesEntity?.IsPublished.Should().BeFalse();
    }

    [Fact]
    public async Task GivenNonExistingArticleAndActiveUser_WhenUpdateArticleContent_ShouldThrowError()
    {
        // Arrange
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

        var users = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Article.Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = userId
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>(); 
        var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

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
            mockedAzureBlobStorageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}