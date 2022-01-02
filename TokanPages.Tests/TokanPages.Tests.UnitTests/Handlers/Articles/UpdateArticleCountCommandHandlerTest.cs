namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Articles;
using Backend.Cqrs.Services.UserServiceProvider;

public class UpdateArticleCountCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenLoggedUserAndExistingArticleAndNoReads_WhenUpdateArticleCount_ShouldReturnSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new Users
        {
            Id = userId,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articleId = Guid.NewGuid();
        var articles = new Articles
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

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserServiceProvider
            .Setup(service => service.GetUserId())
            .ReturnsAsync(userId);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var updateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserServiceProvider.Object);

        var updateArticleCommand = new UpdateArticleCountCommand { Id = articleId };

        // Act
        await updateArticleCountCommandHandler.Handle(updateArticleCommand, CancellationToken.None);

        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleCommand.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articleId)
            .ToListAsync();

        // Assert
        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCount(1);
        articleCounts.Select(counts => counts.ReadCount).First().Should().Be(1);
    }

    [Fact]
    public async Task GivenLoggedUserAndExistingArticleAndExistingReads_WhenUpdateArticleCount_ShouldReturnSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new Users
        {
            Id = userId,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articleId = Guid.NewGuid();
        var articles = new Articles
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

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var articlesCounts = new ArticleCounts
        {
            UserId = userId,
            ArticleId = articleId,
            IpAddress = mockedIpAddress,
            ReadCount = 5
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleCounts.AddAsync(articlesCounts);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var expectedUserReadCount = articlesCounts.ReadCount + 1;
        var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserServiceProvider
            .Setup(service => service.GetUserId())
            .ReturnsAsync(userId);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var updateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserServiceProvider.Object);

        var updateArticleCommand = new UpdateArticleCountCommand { Id = articles.Id };

        // Act
        await updateArticleCountCommandHandler.Handle(updateArticleCommand, CancellationToken.None);

        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleCommand.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articles.Id)
            .ToListAsync();
            
        // Assert
        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCount(1);
        articleCounts.Select(counts => counts.ReadCount).First().Should().Be(expectedUserReadCount);
    }

    [Fact]
    public async Task GivenAnonymousUserAndExistingArticle_WhenUpdateArticleCount_ShouldReturnSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new Users
        {
            Id = userId,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articleId = Guid.NewGuid();
        var articles = new Articles
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

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserServiceProvider
            .Setup(service => service.GetUserId())
            .ReturnsAsync((Guid?)null);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var updateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserServiceProvider.Object);

        var updateArticleCommand = new UpdateArticleCountCommand { Id = articleId };

        // Act
        await updateArticleCountCommandHandler.Handle(updateArticleCommand, CancellationToken.None);

        var articlesEntity = await databaseContext.Articles
            .FindAsync(updateArticleCommand.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articleId)
            .ToListAsync();

        // Assert
        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCount(0);
    }

    [Fact]
    public async Task GivenExistingArticleAndIncorrectArticleId_WhenUpdateArticleCount_ShouldThrowError()
    {
        // Arrange
        var users = new Users
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserServiceProvider
            .Setup(service => service.GetUserId())
            .ReturnsAsync((Guid?)null);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var updateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserServiceProvider.Object);

        var updateArticleCommand = new UpdateArticleCountCommand { Id = Guid.NewGuid() };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => updateArticleCountCommandHandler.Handle(updateArticleCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}