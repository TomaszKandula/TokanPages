using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetArticleInfoQueryHandlerTest : TestBase
{
    private const string UserAlias = "Victoria";

    private const string IpAddressFirst = "255.255.255.255";
        
    private const string IpAddressSecond = "1.1.1.1";

    [Fact]
    public async Task GivenCorrectId_WhenGetArticleInfo_ShouldReturnEntity() 
    {
        // Arrange
        var testDate = DateTime.Now;
        var users = new Backend.Domain.Entities.User.Users
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = UserAlias,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userInfo = new UserInfo
        {
            UserId = users.Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = null,
            UserVideoName = null,
            CreatedBy = Guid.Empty,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            ModifiedBy = null,
            ModifiedAt = null
        };
        
        var articles = new Backend.Domain.Entities.Article.Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = testDate,
            UpdatedAt = null,
            UserId = users.Id,
            LanguageIso = "ENG"
        };

        var likes = new List<ArticleLikes> 
        { 
            new()
            {
                ArticleId = articles.Id,
                UserId = null,
                LikeCount = 10,
                IpAddress = IpAddressFirst
            },
            new()
            {
                ArticleId = articles.Id,
                UserId = null,
                LikeCount = 15,
                IpAddress = IpAddressSecond
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.ArticleLikes.AddRangeAsync(likes);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var query = new GetArticleInfoQuery { Id = articles.Id };
        var handler = new GetArticleInfoQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(articles.Title);
        result.Description.Should().Be(articles.Description);
        result.IsPublished.Should().BeFalse();
        result.ReadCount.Should().Be(articles.ReadCount);
        result.UserLikes.Should().Be(10);
        result.UpdatedAt.Should().BeNull();
        result.CreatedAt.Should().Be(testDate);
        result.TotalLikes.Should().Be(25);
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticleInfo_ShouldThrowError()
    {
        // Arrange
        var users = new Backend.Domain.Entities.User.Users
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
            UserId = users.Id,
            LanguageIso = "ENG"
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var query = new GetArticleInfoQuery { Id = Guid.NewGuid() };
        var handler = new GetArticleInfoQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Contain(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}