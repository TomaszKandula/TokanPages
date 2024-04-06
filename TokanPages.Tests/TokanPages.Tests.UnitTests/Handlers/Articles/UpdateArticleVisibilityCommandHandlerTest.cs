using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class UpdateArticleVisibilityCommandHandlerTest : TestBase
{
    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public async Task GivenCorrectPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldSucceed(bool isVisible, bool shouldBeVisible)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articlesId = Guid.NewGuid();
        var permissionId = Guid.NewGuid();

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articlesId,
            IsPublished = shouldBeVisible
        };

        var users = new Backend.Domain.Entities.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var permission = new Permissions
        {
            Id = permissionId,
            Name = DataUtilityService.GetRandomString()
        };

        var userPermission = new UserPermissions
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId
        };

        var articles = new Backend.Domain.Entities.Articles.Articles
        {
            Id = articlesId,
            UserId = userId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = isVisible,
            ReadCount = 0,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            UpdatedAt = null,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService.Setup(service => service.GetActiveUser(
            It.IsAny<Guid?>(), 
            It.IsAny<bool>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        mockedUserService
            .Setup(service => service.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var articlesEntity = await databaseContext.Articles.FindAsync(articles.Id);

        articlesEntity.Should().NotBeNull();
        articlesEntity?.IsPublished.Should().Be(shouldBeVisible);
    }

    [Fact]
    public async Task GivenNoPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articlesId = Guid.NewGuid();
        var permissionId = Guid.NewGuid();

        var users = new Backend.Domain.Entities.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var permission = new Permissions
        {
            Id = permissionId,
            Name = DataUtilityService.GetRandomString()
        };

        var userPermission = new UserPermissions
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId
        };

        var articles = new Backend.Domain.Entities.Articles.Articles
        {
            Id = articlesId,
            UserId = userId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            UpdatedAt = null,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService.Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articles.Id,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenCorrectPermissionAndWrongArticleId_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articlesId = Guid.NewGuid();
        var permissionId = Guid.NewGuid();

        var users = new Backend.Domain.Entities.Users
        {
            Id = userId,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var permission = new Permissions
        {
            Id = permissionId,
            Name = DataUtilityService.GetRandomString()
        };

        var userPermission = new UserPermissions
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId
        };

        var articles = new Backend.Domain.Entities.Articles.Articles
        {
            Id = articlesId,
            UserId = userId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            UpdatedAt = null,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService.Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}