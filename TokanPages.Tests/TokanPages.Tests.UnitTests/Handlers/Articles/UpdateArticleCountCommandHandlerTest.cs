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
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Handlers.Commands.Articles;

public class UpdateArticleCountCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenLoggedUserAndExistingArticleAndNoReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var handler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            dateTimeService);

        var command = new UpdateArticleCountCommand { Id = articleId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articleId)
            .ToListAsync();

        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCount(1);
        articleCounts.Select(counts => counts.ReadCount).First().Should().Be(1);
    }

    [Fact]
    public async Task GivenLoggedUserAndExistingArticleAndExistingReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleCounts.AddAsync(articlesCounts);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var expectedUserReadCount = articlesCounts.ReadCount + 1;
        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var handler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService);

        var command = new UpdateArticleCountCommand { Id = articles.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articles.Id)
            .ToListAsync();
            
        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCount(1);
        articleCounts.Select(counts => counts.ReadCount).First().Should().Be(expectedUserReadCount);
    }

    [Fact]
    public async Task GivenAnonymousUserAndExistingArticle_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = articles.ReadCount + 1;
        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var command = new UpdateArticleCountCommand { Id = articleId };
        var handler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles
            .FindAsync(command.Id);

        var articleCounts = await databaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == articleId)
            .ToListAsync();

        articlesEntity.Should().NotBeNull();
        articlesEntity.ReadCount.Should().Be(expectedTotalReadCount);
        articleCounts.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GivenExistingArticleAndIncorrectArticleId_WhenUpdateArticleCount_ShouldThrowError()
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
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = user.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var handler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService);

        var command = new UpdateArticleCountCommand { Id = Guid.NewGuid() };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}