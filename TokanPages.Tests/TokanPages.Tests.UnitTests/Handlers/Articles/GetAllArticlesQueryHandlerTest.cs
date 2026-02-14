using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetAllArticlesQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection() 
    {
        // Arrange
        var userId = Guid.NewGuid();

        var articles = new List<Article>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                CreatedAt = DateTime.Now.AddDays(-10),
                CreatedBy = userId,
                UpdatedAt = null,
                UserId = userId,
                LanguageIso = "ENG"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                CreatedAt = DateTime.Now.AddDays(-15),
                CreatedBy = userId,
                UpdatedAt = null,
                UserId = userId,
                LanguageIso = "ENG"
            }
        };

        var articleCategories = new List<ArticleCategory>
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

        var articleIds = new HashSet<Guid>
        {
            articles[0].Id,
            articles[1].Id
        };

        var categoryDto = new List<ArticleCategoryDto>
        {
            new()
            {
                Id = articleCategories[0].Id,
                CategoryName =  DataUtilityService.GetRandomString(),
            },
            new()
            {
                Id = articleCategories[1].Id,
                CategoryName =  DataUtilityService.GetRandomString(),
            }
        };

        var articleDataDto = new List<ArticleDataDto>
        {
            new()
            {
                Id = articles[0].Id,
                CategoryName = DataUtilityService.GetRandomString(),
                Title =  DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 8436,
                TotalLikes = 234,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                LanguageIso = "ENG"
            },
            new()
            {
                Id = articles[1].Id,
                CategoryName = DataUtilityService.GetRandomString(),
                Title =  DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 9642,
                TotalLikes = 679,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                LanguageIso = "ENG"
            }
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserProvider = new Mock<IUserService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedUserProvider
            .Setup(service => service.GetRequestUserLanguage())
            .Returns("en");

        mockedArticlesRepository
            .Setup(repository => repository.GetSearchResult(It.IsAny<string>()))
            .ReturnsAsync(articleIds);

        mockedArticlesRepository
            .Setup(repository => repository.GetArticleList(
            It.IsAny<bool>(),
            It.IsAny<string?>(),
            It.IsAny<Guid?>(),
            It.IsAny<HashSet<Guid>?>(),
            It.IsAny<ArticlePageInfoDto>()
            ))
            .ReturnsAsync(articleDataDto);

        mockedArticlesRepository
            .Setup(repository => repository.GetArticleCategories(It.IsAny<string>()))
            .ReturnsAsync(categoryDto);

        var query = new GetArticlesQuery
        {
            IsPublished = false,
            PageNumber = 1,
            PageSize = 10,
        };

        var handler = new GetArticlesQueryHandler(mockedLogger.Object, mockedUserProvider.Object, mockedArticlesRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Results.Should().NotBeNull();
        result.Results?.Count.Should().Be(2);
        result.PagingInfo.Should().NotBeNull();
        result.PagingInfo?.PageNumber.Should().Be(1);
        result.PagingInfo?.PageSize.Should().Be(10);
        result.ArticleCategories.Should().HaveCount(2);
    }
}