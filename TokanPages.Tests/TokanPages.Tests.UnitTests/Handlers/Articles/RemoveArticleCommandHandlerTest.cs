using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class RemoveArticleCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveArticle_ShouldRemoveEntity() 
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var users = new Backend.Domain.Entities.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Articles
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

        var articleLikes = new ArticleLikes
        {
            ArticleId = articleId,
            UserId = userId,
            IpAddress = DataUtilityService.GetRandomIpAddress().ToString(),
            LikeCount = DataUtilityService.GetRandomInteger()
        };

        var articleCounts = new ArticleCounts
        {
            ArticleId = articleId,
            UserId = userId,
            IpAddress = DataUtilityService.GetRandomIpAddress().ToString(),
            ReadCount = DataUtilityService.GetRandomInteger()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleLikes.AddAsync(articleLikes);
        await databaseContext.ArticleCounts.AddAsync(articleCounts);        
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        var command = new RemoveArticleCommand { Id = articleId };
        var handler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act 
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articleCheck = await databaseContext.Articles.Where(x => x.Id == command.Id).ToListAsync();
        var articleCountCheck = await databaseContext.ArticleCounts.Where(x => x.ArticleId == command.Id).ToListAsync();
        var articleLikeCheck = await databaseContext.ArticleLikes.Where(x => x.ArticleId == command.Id).ToListAsync();

        articleCheck.Should().BeEmpty();
        articleCountCheck.Should().BeEmpty();
        articleLikeCheck.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new Backend.Domain.Entities.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Articles
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

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        var command = new RemoveArticleCommand { Id = Guid.NewGuid() };
        var handler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}