using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class UpdateArticleCountCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenAnonymousUserAndExistingArticleAndNoReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articleId = Guid.NewGuid();
        var articles = new Backend.Domain.Entities.Article.Articles
        {
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = userId,
            LanguageIso = "ENG"
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
            .Setup(service => service.GetUser(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserOutput)null!);

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
        var entity = await databaseContext.Articles.FindAsync(command.Id);
        entity.Should().NotBeNull();
        entity?.ModifiedBy.Should().NotBeNull();
        entity?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
        entity?.ReadCount.Should().Be(expectedTotalReadCount);

        var counts = await databaseContext.ArticleCounts.SingleOrDefaultAsync(x => x.ArticleId == articleId);
        counts.Should().NotBeNull();
        counts?.ArticleId.Should().Be(articleId);
        counts?.UserId.Should().Be(userId);
        counts?.IpAddress.Should().Be(mockedIpAddress);
        counts?.ReadCount.Should().Be(1);
        counts?.CreatedBy.Should().Be(Guid.Empty);
        counts?.CreatedAt.Should().BeBefore(DateTime.UtcNow);
        counts?.ModifiedBy.Should().BeNull();
        counts?.ModifiedAt.Should().BeNull();
    }

    [Fact]
    public async Task GivenAnonymousUserAndExistingArticleAndReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articleId = Guid.NewGuid();
        var article = new Backend.Domain.Entities.Article.Articles
        {
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 10,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = userId,
            LanguageIso = "ENG"
        };

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var articleCount = new ArticleCounts
        {
            Id = Guid.NewGuid(),
            ArticleId = articleId,
            UserId = userId,
            IpAddress = mockedIpAddress,
            ReadCount = 10,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = userId,
            ModifiedAt = null,
            ModifiedBy = null
        };
        
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.ArticleCounts.AddAsync(articleCount);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = article.ReadCount + 1;
        var expectedReadCount = articleCount.ReadCount + 1;
        
        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetUser(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserOutput)null!);

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
        var entity = await databaseContext.Articles.FindAsync(command.Id);
        entity.Should().NotBeNull();
        entity?.UpdatedAt.Should().BeNull();
        entity?.ModifiedBy.Should().NotBeNull();
        entity?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
        entity?.ReadCount.Should().Be(expectedTotalReadCount);

        var counts = await databaseContext.ArticleCounts.SingleOrDefaultAsync(x => x.ArticleId == articleId);
        counts.Should().NotBeNull();
        counts?.ArticleId.Should().Be(articleId);
        counts?.UserId.Should().Be(userId);
        counts?.IpAddress.Should().Be(mockedIpAddress);
        counts?.ReadCount.Should().Be(expectedReadCount);
        counts?.CreatedBy.Should().Be(articleCount.CreatedBy);
        counts?.CreatedAt.Should().Be(articleCount.CreatedAt);
        counts?.ModifiedBy.Should().BeNull();
        counts?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public async Task GivenLoggedUserAndExistingArticleAndExistingReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articleId = Guid.NewGuid();
        var article = new Backend.Domain.Entities.Article.Articles
        {
            Id = articleId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 5,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = userId,
            LanguageIso = "ENG"
        };

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var articleCount = new ArticleCounts
        {
            UserId = userId,
            ArticleId = articleId,
            IpAddress = mockedIpAddress,
            ReadCount = 5
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.ArticleCounts.AddAsync(articleCount);
        await databaseContext.SaveChangesAsync();

        var expectedTotalReadCount = article.ReadCount + 1;
        var expectedUserReadCount = articleCount.ReadCount + 1;

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var getUserDto = new GetUserOutput
        {
            UserId = userId,
            AliasName = DataUtilityService.GetRandomString(),
            AvatarName = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Email = DataUtilityService.GetRandomEmail(),
            ShortBio = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime()
        };

        mockedUserService
            .Setup(service => service.GetUser(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(getUserDto);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var handler = new UpdateArticleCountCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            dateTimeService);

        var command = new UpdateArticleCountCommand { Id = article.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var entity = await databaseContext.Articles.FindAsync(command.Id);
        entity.Should().NotBeNull();
        entity?.UpdatedAt.Should().BeNull();
        entity?.ModifiedBy.Should().NotBeNull();
        entity?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
        entity?.ReadCount.Should().Be(expectedTotalReadCount);

        var count = await databaseContext.ArticleCounts.SingleOrDefaultAsync(counts => counts.ArticleId == article.Id);
        count.Should().NotBeNull();
        count?.ArticleId.Should().Be(articleId);
        count?.UserId.Should().Be(userId);
        count?.IpAddress.Should().Be(mockedIpAddress);
        count?.ReadCount.Should().Be(expectedUserReadCount);
        count?.CreatedBy.Should().Be(articleCount.CreatedBy);
        count?.CreatedAt.Should().Be(articleCount.CreatedAt);
        count?.ModifiedBy.Should().Be(articleCount.UserId);
        count?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public async Task GivenExistingArticleAndIncorrectArticleId_WhenUpdateArticleCount_ShouldThrowError()
    {
        // Arrange
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = Guid.NewGuid(),
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
            UserId = user.Id,
            LanguageIso = "ENG"
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

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