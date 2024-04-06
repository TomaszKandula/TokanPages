using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetAllArticlesQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection() 
    {
        // Arrange
        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            UserAlias  = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new List<Backend.Domain.Entities.Articles.Articles>
        {
            new()
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = null,
                UserId = user.Id
            },
            new()
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now.AddDays(-15),
                UpdatedAt = null,
                UserId = user.Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddRangeAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetAllArticlesQuery { IsPublished = false };
        var handler = new GetAllArticlesQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ToList().Should().NotBeNull();
        result.ToList().Should().HaveCount(2);
        result.ToList()[0].Id.Should().Be(articles[0].Id);
        result.ToList()[1].Id.Should().Be(articles[1].Id);
    }
}