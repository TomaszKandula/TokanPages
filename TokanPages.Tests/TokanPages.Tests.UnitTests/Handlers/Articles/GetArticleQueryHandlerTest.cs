using System.Text;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetArticleQueryHandlerTest : TestBase
{
    private const string UserAlias = "Victoria";

    private const string IpAddress = "255.255.255.255";

    [Fact]
    public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntity() 
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext(); //TODO: to be removed
        var testDate = DateTime.Now;
        var users = new User
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = UserAlias,
            CryptedPassword = DataUtilityService.GetRandomString()
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

        var categoryNames = new List<ArticleCategoryName>
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

        var articles = new Article
        {
            Id = Guid.NewGuid(),
            CategoryId = articleCategories[0].Id,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = testDate,
            UpdatedAt = null,
            UserId = users.Id,
            LanguageIso = "ENG"
        };

        var articleOutputDto = new GetArticleOutputDto
        {
            Id =  articles.Id,
            Title = articles.Title,
            Description = articles.Description,
            IsPublished = articles.IsPublished,
            ReadCount = articles.ReadCount,
            TotalLikes = articles.TotalLikes,
            CreatedAt = testDate,
            UpdatedAt = testDate,
            LanguageIso = "ENG",
            UserLikes = 1100,
            CategoryName =  categoryNames[0].Name
        };

        var mockedUserProvider = new Mock<IUserService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();
        var mockedArticleRepository = new Mock<IArticlesRepository>();

        var mockedArticleText = GetArticleSectionsAsJson();
        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedArticleText);

        mockedAzureStorage
            .Setup(factory => factory.Create(mockedLogger.Object))
            .Returns(mockedAzureBlob.Object);

        mockedUserProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(IpAddress);

        mockedUserProvider
            .Setup(service => service.GetRequestUserLanguage())
            .Returns("en");

        var articleId = Guid.NewGuid();
        mockedArticleRepository
            .Setup(repository => repository.GetArticleIdByTitle(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(articleId);

        mockedArticleRepository
            .Setup(repository => repository.GetArticle(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(articleOutputDto);

        var query = new GetArticleQuery { Id = articles.Id };
        var handler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedAzureStorage.Object, 
            mockedArticleRepository.Object);

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
        result.CategoryName.Should().Be(categoryNames[0].Name);
        result.Author.Should().NotBeNull();
        result.Author?.AliasName.Should().Be(UserAlias);
        result.Author?.AvatarName.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext(); //TODO: to be removed

        var mockedUserProvider = new Mock<IUserService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();
        var mockedArticleRepository = new Mock<IArticlesRepository>();

        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StorageStreamContent)null!);

        mockedAzureStorage
            .Setup(factory => factory.Create(mockedLogger.Object))
            .Returns(mockedAzureBlob.Object);
        
        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddress);

        var articleId = Guid.NewGuid();
        mockedArticleRepository
            .Setup(repository => repository.GetArticleIdByTitle(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(articleId);

        GetArticleOutputDto? articleOutputDto = null;
        mockedArticleRepository
            .Setup(repository => repository.GetArticle(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(articleOutputDto);

        var query = new GetArticleQuery { Id = Guid.NewGuid() };
        var handler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedAzureStorage.Object, 
            mockedArticleRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Contain(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }

    private StorageStreamContent GetArticleSectionsAsJson()
    {
        var sections = new List<ArticleSectionDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Type = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Value = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Prop = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Text = DataUtilityService.GetRandomString(useAlphabetOnly: true)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Value = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Prop = DataUtilityService.GetRandomString(useAlphabetOnly: true),
                Text = DataUtilityService.GetRandomString(useAlphabetOnly: true)
            }
        };

        var serialized = JsonConvert.SerializeObject(sections);
        var bytes = Encoding.Default.GetBytes(serialized);
        
        return new StorageStreamContent
        {
            Content = new MemoryStream(bytes),
            ContentType = "application/json"
        };
    }
}