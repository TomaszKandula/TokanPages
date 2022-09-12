using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.UserService;
using TokanPages.WebApi.Dto.Content.Common;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class GetArticleQueryHandlerTest : TestBase
{
    private const string UserAlias = "Ester";

    private const string IpAddressFirst = "255.255.255.255";
        
    private const string IpAddressSecond = "1.1.1.1";

    [Fact]
    public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntity() 
    {
        // Arrange
        var testDate = DateTime.Now;
        var users = new Backend.Domain.Entities.Users
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
        
        var articles = new Backend.Domain.Entities.Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = testDate,
            UpdatedAt = null,
            UserId = users.Id
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
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();

        var mockedArticleText = GetArticleSectionsAsJson();
        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedArticleText);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlob.Object);

        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var query = new GetArticleQuery { Id = articles.Id };
        var handler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedAzureStorage.Object);

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
        result.LikeCount.Should().Be(25);
        result.Author.Should().NotBeNull();
        result.Author?.AliasName.Should().Be(UserAlias);
        result.Author?.AvatarName.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var users = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var articles = new Backend.Domain.Entities.Articles
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserProvider = new Mock<IUserService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();

        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StorageStreamContent)null!);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlob.Object);
        
        mockedUserProvider
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(IpAddressFirst);

        var query = new GetArticleQuery { Id = Guid.NewGuid() };
        var handler = new GetArticleQueryHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object, 
            mockedJsonSerializer.Object, 
            mockedAzureStorage.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Contain(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }

    private StorageStreamContent GetArticleSectionsAsJson()
    {
        var sections = new List<Section>
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