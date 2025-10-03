using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class RetrieveArticleInfoCommandHandlerTest : TestBase
{
    private const string UserAlias = "Victoria";

    private const string IpAddressFirst = "255.255.255.255";

    private const string IpAddressSecond = "1.1.1.1";

    [Fact]
    public async Task GivenCorrectIds_WhenRetrieveArticleInfo_ShouldReturnEntities() 
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

        var categories = new ArticleCategory
        {
            Id = Guid.NewGuid(),
            CategoryName = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Article.Articles
        {
            Id = Guid.NewGuid(),
            CategoryId = categories.Id,
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
        await databaseContext.ArticleCategory.AddAsync(categories);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var query = new RetrieveArticleInfoCommand 
        { 
            ArticleIds = new List<Guid>
            {
                articles.Id
            }
        };

        var handler = new RetrieveArticleInfoCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Articles.Should().NotBeNull();
        result.Articles.Count.Should().Be(1);
        result.Articles[0].CategoryName.Should().Be(categories.CategoryName);
        result.Articles[0].Title.Should().Be(articles.Title);
        result.Articles[0].Description.Should().Be(articles.Description);
        result.Articles[0].IsPublished.Should().BeFalse();
        result.Articles[0].ReadCount.Should().Be(articles.ReadCount);
        result.Articles[0].UpdatedAt.Should().BeNull();
        result.Articles[0].CreatedAt.Should().Be(testDate);
        result.Articles[0].TotalLikes.Should().Be(25);
    }
}