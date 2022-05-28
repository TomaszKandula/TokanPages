namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using Backend.Domain.Entities;
using Backend.Shared.Resources;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Articles;
using UsersEntity = Backend.Domain.Entities.Users;
using ArticlesEntity = Backend.Domain.Entities.Articles;
using PermissionsEntity = Backend.Domain.Entities.Permissions;
using AuthorizationPermissions = Backend.Domain.Enums.Permissions;

public class UpdateArticleVisibilityCommandHandlerTest : TestBase
{
    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public async Task GivenCorrectPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldFinishSuccess(bool isVisible, bool shouldBeVisible)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId);
        var permission = GetPermission();
        var userPermission = GetUserPermission(userId, permission.Id);
        var articles = GetUserArticle(userId, isVisible);

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();
            
        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.HasPermissionAssigned(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var updateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
        {
            Id = articles.Id,
            IsPublished = shouldBeVisible
        };

        var updateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, mockedLogger.Object, mockedUserProvider.Object);
            
        // Act
        await updateArticleVisibilityCommandHandler.Handle(updateArticleVisibilityCommand, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles
            .FindAsync(articles.Id);

        articlesEntity.Should().NotBeNull();
        articlesEntity.IsPublished.Should().Be(shouldBeVisible);
    }

    [Fact]
    public async Task GivenNoPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId);
        var permission = GetPermission();
        var userPermission = GetUserPermission(userId, permission.Id);
        var articles = GetUserArticle(userId, false);

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();
            
        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.HasPermissionAssigned(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var updateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
        {
            Id = articles.Id,
            IsPublished = true
        };

        var updateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, mockedLogger.Object, mockedUserProvider.Object);
            
        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => 
            updateArticleVisibilityCommandHandler.Handle(updateArticleVisibilityCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenCorrectPermissionAndWrongArticleId_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId);
        var permission = GetPermission();
        var userPermission = GetUserPermission(userId, permission.Id);
        var articles = GetUserArticle(userId, false);

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();
            
        var mockedUserProvider = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.HasPermissionAssigned(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var updateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };

        var updateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, mockedLogger.Object, mockedUserProvider.Object);
            
        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => 
            updateArticleVisibilityCommandHandler.Handle(updateArticleVisibilityCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
        
    private ArticlesEntity GetUserArticle(Guid userId, bool isPublished)
    {
        return new ()
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = isPublished,
            ReadCount = 0,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            UpdatedAt = null,
            UserId = userId
        };
    }

    private UsersEntity GetUser(Guid userId)
    {
        return new Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };
    }

    private static PermissionsEntity GetPermission()
    {
        return new Permissions
        {
            Id = Guid.NewGuid(),
            Name = AuthorizationPermissions.CanPublishArticles.ToString()
        };
    }

    private static UserPermissions GetUserPermission(Guid userId, Guid permissionId)
    {
        return new UserPermissions
        {
            UserId = userId,
            PermissionId = permissionId
        };
    }
}