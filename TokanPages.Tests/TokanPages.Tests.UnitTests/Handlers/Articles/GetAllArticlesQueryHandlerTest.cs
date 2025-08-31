﻿using FluentAssertions;
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
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = Guid.NewGuid(),
            UserAlias  = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new List<Backend.Domain.Entities.Article.Articles>
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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.Articles.AddRangeAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetArticlesQuery
        {
            IsPublished = false,
            PageNumber = 1,
            PageSize = 10,
        };

        var handler = new GetArticlesQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Results.Should().NotBeNull();
        result.Results?.Count.Should().Be(2);
        result.PagingInfo.Should().NotBeNull();
        result.PagingInfo?.PageNumber.Should().Be(1);
        result.PagingInfo?.PageSize.Should().Be(10);
    }
}