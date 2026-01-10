using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetAllArticlesQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection() 
    {
        // Arrange
        var user = new Backend.Domain.Entities.User.User
        {
            Id = Guid.NewGuid(),
            UserAlias  = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new List<Backend.Domain.Entities.Article.Article>
        {
            new()
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = null,
                UserId = user.Id,
                LanguageIso = "ENG"
            },
            new()
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now.AddDays(-15),
                UpdatedAt = null,
                UserId = user.Id,
                LanguageIso = "ENG"
            }
        };

        var languages = new List<Backend.Domain.Entities.Language>
        {
            new()
            {
                Id = Guid.NewGuid(),
                LangId = "en",
                HrefLang = "en-GB",
                Name = "English",
                IsDefault = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                LangId = "pl",
                HrefLang = "pl-PL",
                Name = "Polski",
                IsDefault = false
            }
        };

        var articleCategories = new List<Backend.Domain.Entities.Article.ArticleCategory>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTimeService.Now
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTimeService.Now
            },
        };
        
        var categoryNames = new List<Backend.Domain.Entities.CategoryName>
        {
            new()
            {
                ArticleCategoryId = articleCategories[0].Id,
                LanguageId = languages[0].Id,
                Name = DataUtilityService.GetRandomString()
            },
            new()
            {
                ArticleCategoryId = articleCategories[1].Id,
                LanguageId = languages[1].Id,
                Name = DataUtilityService.GetRandomString()
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Languages.AddRangeAsync(languages);
        await databaseContext.Articles.AddRangeAsync(articles);
        await databaseContext.ArticleCategory.AddRangeAsync(articleCategories);
        await databaseContext.CategoryNames.AddRangeAsync(categoryNames);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserProvider = new Mock<IUserService>();

        mockedUserProvider
            .Setup(service => service.GetRequestUserLanguage())
            .Returns("en");

        var query = new GetArticlesQuery
        {
            IsPublished = false,
            PageNumber = 1,
            PageSize = 10,
        };

        var handler = new GetArticlesQueryHandler(databaseContext, mockedLogger.Object, mockedUserProvider.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Results.Should().NotBeNull();
        result.Results?.Count.Should().Be(2);
        result.PagingInfo.Should().NotBeNull();
        result.PagingInfo?.PageNumber.Should().Be(1);
        result.PagingInfo?.PageSize.Should().Be(10);
        result.ArticleCategories.Should().HaveCount(1);
    }
}