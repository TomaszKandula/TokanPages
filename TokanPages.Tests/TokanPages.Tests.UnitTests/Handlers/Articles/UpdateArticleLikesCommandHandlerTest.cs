using TokanPages.WebApi.Dto.Users;

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
using Backend.Core.Utilities.DateTimeService;
using Backend.Application.Handlers.Commands.Articles;
using TokanPages.Backend.Shared.Services.Models;

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
        var user = new Users
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var article = new Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = user.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.SaveChangesAsync();

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var likesLimit = new LimitSettings { Likes = new Likes { ForAnonymous = 25, ForUser = 50 } };
        var mockedApplicationSettings = MockApplicationSettings(limitSettings: likesLimit);

        mockedUserService
            .Setup(service => service.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserDto)null!);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(IpAddress);

        var command = new UpdateArticleLikesCommand
        {
            Id = article.Id,
            AddToLikes = likes,
        };

        var handler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService, 
            mockedApplicationSettings.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articleLikesEntity = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == article.Id)
            .ToList();

        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

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
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        var likes = new ArticleLikes 
        { 
            Id = Guid.NewGuid(),
            ArticleId = articles.Id,
            UserId = null,
            IpAddress = IpAddress,
            LikeCount = existingLikes
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleLikes.AddAsync(likes);
        await databaseContext.SaveChangesAsync();

        var dateTimeService = new DateTimeService();
        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var likesLimit = new LimitSettings { Likes = new Likes { ForAnonymous = 25, ForUser = 50 } };
        var mockedApplicationSettings = MockApplicationSettings(limitSettings: likesLimit);

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var command = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = newLikes,
        };

        var handler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserProvider.Object, 
            dateTimeService,
            mockedApplicationSettings.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articleLikesEntity = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();

        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

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
        var user = new Users
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var article = new Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = user.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.SaveChangesAsync();

        var getUserDto = new GetUserDto
        {
            UserId = user.Id,
            AliasName = DataUtilityService.GetRandomString(),
            AvatarName = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Email = DataUtilityService.GetRandomEmail(),
            ShortBio = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime()
        };

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var likesLimit = new LimitSettings { Likes = new Likes { ForAnonymous = 25, ForUser = 50 } };
        var mockedApplicationSettings = MockApplicationSettings(limitSettings: likesLimit);

        mockedUserService
            .Setup(provider => provider.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync(getUserDto);

        mockedUserService
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var command = new UpdateArticleLikesCommand
        {
            Id = article.Id,
            AddToLikes = likes,
        };

        var handler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService,
            mockedApplicationSettings.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articleLikes = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == article.Id)
            .ToList();

        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

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
        var user = new Users
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = user.Id
        };
            
        var likes = new ArticleLikes 
        { 
            Id = Guid.NewGuid(),
            ArticleId = articles.Id,
            UserId = user.Id,
            IpAddress = IpAddress,
            LikeCount = existingLikes
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleLikes.AddAsync(likes);
        await databaseContext.SaveChangesAsync();

        var getUserDto = new GetUserDto
        {
            UserId = user.Id,
            AliasName = DataUtilityService.GetRandomString(),
            AvatarName = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Email = DataUtilityService.GetRandomEmail(),
            ShortBio = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime()
        };

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var likesLimit = new LimitSettings { Likes = new Likes { ForAnonymous = 25, ForUser = 50 } };
        var mockedApplicationSettings = MockApplicationSettings(limitSettings: likesLimit);

        mockedUserService
            .Setup(provider => provider.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync(getUserDto);

        mockedUserService
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var command = new UpdateArticleLikesCommand
        {
            Id = articles.Id,
            AddToLikes = newLikes
        };

        var handler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService,
            mockedApplicationSettings.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articleLikes = databaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.ArticleId == articles.Id)
            .ToList();
            
        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

        articlesEntity.Should().NotBeNull();
        articleLikes.Should().HaveCount(1);
        articleLikes[0].UserId.Should().NotBeNull();
        articleLikes[0].LikeCount.Should().Be(expectedLikes);
    }
}