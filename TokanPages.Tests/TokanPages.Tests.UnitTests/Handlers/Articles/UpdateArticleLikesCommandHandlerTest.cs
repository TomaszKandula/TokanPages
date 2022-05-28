namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Articles;

public class UpdateArticleLikesCommandHandlerTest : TestBase
{
    private const string IpAddress = "255.255.255.255";
        
    [Theory]
    [InlineData(10, 10)]
    [InlineData(50, 25)]
    [InlineData(70, 25)]
    public async Task GivenNewLikesAddedAsAnonymousUser_WhenUpdateArticleLikes_ShouldAddLikes(int likes, int expectedLikes)
    {
        // Arrange
        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var updateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        var updateArticleLikesCommand = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = likes,
        };

        // Act
        await updateArticleLikesCommandHandler.Handle(updateArticleLikesCommand, CancellationToken.None);

        // Assert
        var articleLikesEntity = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();

        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleLikesCommand.Id);

        articlesEntity.Should().NotBeNull();

        articleLikesEntity.Should().HaveCount(1);
        articleLikesEntity[0].IpAddress.Should().Be(IpAddress);
        articleLikesEntity[0].UserId.Should().BeNull();
        articleLikesEntity[0].LikeCount.Should().Be(expectedLikes);
    }

    [Theory]
    [InlineData(10, 10, 20)]
    [InlineData(50, 10, 25)]
    [InlineData(70, 10, 25)]
    public async Task GivenExistingLikesUpdatedAsAnonymousUser_WhenUpdateArticleLikes_ShouldModifyLikes(int existingLikes, int newLikes, int expectedLikes)
    {
        // Arrange
        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };
            
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var likes = new ArticleLikes 
        { 
            ArticleId = articles.Id,
            UserId = null,
            IpAddress = IpAddress,
            LikeCount = existingLikes
        };

        await databaseContext.ArticleLikes.AddAsync(likes);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var updateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserProvider.Object);

        var updateArticleLikesCommand = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = newLikes,
        };

        // Act
        await updateArticleLikesCommandHandler.Handle(updateArticleLikesCommand, CancellationToken.None);

        // Assert
        var articleLikesEntity = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();
            
        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleLikesCommand.Id);

        articlesEntity.Should().NotBeNull();
        articleLikesEntity.Should().HaveCount(1);
        articleLikesEntity[0].IpAddress.Should().Be(IpAddress);
        articleLikesEntity[0].UserId.Should().BeNull();
        articleLikesEntity[0].LikeCount.Should().Be(expectedLikes);
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(50, 50)]
    [InlineData(70, 50)]
    public async Task GivenNewLikesAddedAsLoggedUser_WhenUpdateArticleLikes_ShouldAddLikes(int likes, int expectedLikes)
    {
        // Arrange
        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetUserId(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users.Id);

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var updateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        var updateArticleLikesCommand = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = likes,
        };

        // Act
        await updateArticleLikesCommandHandler.Handle(updateArticleLikesCommand, CancellationToken.None);

        // Assert
        var articleLikes = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();

        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleLikesCommand.Id);

        articlesEntity.Should().NotBeNull();
        articleLikes.Should().HaveCount(1);
        articleLikes[0].UserId.Should().NotBeNull();
        articleLikes[0].LikeCount.Should().Be(expectedLikes);
    }

    [Theory]
    [InlineData(10, 10, 20)]
    [InlineData(50, 10, 50)]
    [InlineData(70, 10, 50)]
    public async Task GivenExistingLikesUpdatedAsLoggedUser_WhenUpdateArticleLikes_ShouldModifyLikes(int existingLikes, int newLikes, int expectedLikes)
    {
        // Arrange
        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };
            
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var likes = new ArticleLikes 
        { 
            ArticleId = articles.Id,
            UserId = users.Id,
            IpAddress = IpAddress,
            LikeCount = existingLikes
        };

        await databaseContext.ArticleLikes.AddAsync(likes);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetUserId(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users.Id);

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var updateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        var updateArticleLikesCommand = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = newLikes,
        };

        // Act
        await updateArticleLikesCommandHandler.Handle(updateArticleLikesCommand, CancellationToken.None);

        // Assert
        var articleLikes = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();
            
        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleLikesCommand.Id);

        articlesEntity.Should().NotBeNull();
        articleLikes.Should().HaveCount(1);
        articleLikes[0].UserId.Should().NotBeNull();
        articleLikes[0].LikeCount.Should().Be(expectedLikes);
    }
}