namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.JsonSerializer;
using Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Models;

public class GetArticleQueryHandlerTest : TestBase
{
    private const string UserAlias = "Ester";

    private const string IpAddressFirst = "255.255.255.255";
        
    private const string IpAddressSecond = "1.1.1.1";

    [Fact]
    public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntity() 
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var testDate = DateTime.Now;
            
        var users = new Users
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = UserAlias,
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = testDate,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var likes = new List<ArticleLikes> 
        { 
            new ()
            {
                ArticleId = articles.Id,
                UserId = null,
                LikeCount = 10,
                IpAddress = IpAddressFirst
            },
            new ()
            {
                ArticleId = articles.Id,
                UserId = null,
                LikeCount = 15,
                IpAddress = IpAddressSecond
            }
        };
            
        await databaseContext.ArticleLikes.AddRangeAsync(likes);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedCustomHttpClient = new Mock<IHttpClientService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedApplicationSettings = MockApplicationSettings();
            
        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);

        var getArticleQuery = new GetArticleQuery { Id = articles.Id };
        var getArticleQueryHandler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedCustomHttpClient.Object, 
            mockedApplicationSettings.Object);

        // Act
        var result = await getArticleQueryHandler.Handle(getArticleQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(articles.Title);
        result.Description.Should().Be(articles.Description);
        result.IsPublished.Should().BeFalse();
        result.ReadCount.Should().Be(articles.ReadCount);
        result.UserLikes.Should().Be(10);
        result.UpdatedAt.Should().BeNull();
        result.CreatedAt.Should().Be(testDate);
        result.LikeCount.Should().Be(25);
        result.Author.AliasName.Should().Be(UserAlias);
        result.Author.AvatarName.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery
        {
            Id = Guid.Parse("9bc64ac6-cb57-448e-81b7-32f9a8f2f27c")
        };

        var users = new Users
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            Registered = DataUtilityService.GetRandomDateTime(),
            LastLogged = null,
            LastUpdated = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };
            
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedCustomHttpClient = new Mock<IHttpClientService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);
            
        var getArticleQueryHandler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedCustomHttpClient.Object, 
            mockedApplicationSettings.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => getArticleQueryHandler.Handle(getArticleQuery, CancellationToken.None));
    }
}