using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class RetrieveArticleInfoCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectIds_WhenRetrieveArticleInfo_ShouldReturnEntities() 
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        const string ipAddress = "255.255.255.255";
        var testDate = DateTime.Now;
        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticleRepository = new Mock<IArticlesRepository>();

        var articles = new List<ArticleDataDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CategoryName = DataUtilityService.GetRandomString(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = testDate,
                UpdatedAt = null,
                LanguageIso = "ENG"
            },
            new()
            {
                Id = Guid.NewGuid(),
                CategoryName = DataUtilityService.GetRandomString(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = testDate,
                UpdatedAt = null,
                LanguageIso = "ENG"
            },
        };

        mockedArticleRepository
            .Setup(repository => repository.RetrieveArticleInfo(
                It.IsAny<string>(), 
                It.IsAny<HashSet<Guid>>()))
            .ReturnsAsync(articles);

        mockedUserProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserProvider
            .Setup(service => service.GetRequestUserLanguage())
            .Returns("en");

        var query = new RetrieveArticleInfoCommand 
        { 
            ArticleIds = new List<Guid>
            {
                articles[0].Id,
                articles[1].Id,
            }
        };

        var handler = new RetrieveArticleInfoCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedArticleRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Articles.Should().NotBeNull();
        result.Articles.Count.Should().Be(2);
    }
}