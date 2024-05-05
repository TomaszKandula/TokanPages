using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class RemoveUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users 
        { 
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var role = new Roles
        {
            Id = Guid.NewGuid(),
            Name = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString()
        };

        var permission = new Permissions
        {
            Id = Guid.NewGuid(),
            Name = DataUtilityService.GetRandomString(),
        };

        var photoGear = new PhotoGears
        { 
            Id = Guid.NewGuid(),
            Aperture = DataUtilityService.GetRandomDecimal(),
            BodyModel = DataUtilityService.GetRandomString(),
            BodyVendor = DataUtilityService.GetRandomString(),
            FilmIso = DataUtilityService.GetRandomInteger(),
            FocalLength = DataUtilityService.GetRandomInteger(),
            LensName = DataUtilityService.GetRandomString(),
            LensVendor = DataUtilityService.GetRandomString(),
            ShutterSpeed = DataUtilityService.GetRandomString()
        };

        var photoCategory = new PhotoCategories
        {
            Id = Guid.NewGuid(),
            CategoryName = DataUtilityService.GetRandomString()
        };
        
        var userPhoto = new UserPhotos
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PhotoGearId = photoGear.Id,
            PhotoCategoryId = photoCategory.Id,
            Keywords = DataUtilityService.GetRandomString(),
            PhotoUrl = DataUtilityService.GetRandomString(),
            DateTaken = DataUtilityService.GetRandomDateTime(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString()
        };

        var userInfo = new UserInfo
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            UserVideoName = DataUtilityService.GetRandomString(),
            CreatedBy = Guid.NewGuid(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            ModifiedBy = null,
            ModifiedAt = null,
        };

        var userToken = new UserTokens
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = DataUtilityService.GetRandomString(),
            Expires = DataUtilityService.GetRandomDateTime(),
            Created = DataUtilityService.GetRandomDateTime(),
            CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
            Command = DataUtilityService.GetRandomString(),
            Revoked = null,
            RevokedByIp = null,
            ReasonRevoked = null,
        };

        var userRefreshToken = new UserRefreshTokens
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = DataUtilityService.GetRandomString(),
            Expires = DataUtilityService.GetRandomDateTime(),
            Created = DataUtilityService.GetRandomDateTime(),
            CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
            Revoked = null,
            RevokedByIp = null,
            ReasonRevoked = null,
        };

        var userRole = new UserRoles
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RoleId = role.Id
        };

        var userPermission = new UserPermissions
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permission.Id
        };

        var album = new Albums
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserPhotoId = userPhoto.Id,
            Title = DataUtilityService.GetRandomString()
        };

        var article = new Backend.Domain.Entities.Article.Articles
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = true,
            ReadCount = DataUtilityService.GetRandomInteger(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            UpdatedAt = null,
            LanguageIso = "ENG"
        };

        var articleLike = new ArticleLikes
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ArticleId = article.Id,
            IpAddress = DataUtilityService.GetRandomIpAddress().ToString(),
            LikeCount = DataUtilityService.GetRandomInteger()
        };

        var articleCount = new ArticleCounts
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ArticleId= article.Id,
            IpAddress= DataUtilityService.GetRandomIpAddress().ToString(),
            ReadCount= DataUtilityService.GetRandomInteger()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Roles.AddAsync(role);
        await databaseContext.Permissions.AddAsync(permission);
        await databaseContext.PhotoGears.AddAsync(photoGear);
        await databaseContext.PhotoCategories.AddAsync(photoCategory);
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserPhotos.AddAsync(userPhoto);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.UserTokens.AddAsync(userToken);
        await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
        await databaseContext.UserRoles.AddAsync(userRole);
        await databaseContext.UserPermissions.AddAsync(userPermission);
        await databaseContext.Albums.AddAsync(album);
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.ArticleLikes.AddAsync(articleLike);
        await databaseContext.ArticleCounts.AddAsync(articleCount);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new RemoveUserCommand { Id = userId };
        var handler = new RemoveUserCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var users = await databaseContext.Users.FindAsync(userId);
        var albums = await databaseContext.Albums.Where(x => x.UserId == userId).ToListAsync();
        var articles = await databaseContext.Articles.Where(x => x.UserId == userId).ToListAsync();
        var articleLikes = await databaseContext.ArticleLikes.Where(x => x.UserId == userId).ToListAsync();
        var articleCounts = await databaseContext.ArticleCounts.Where(x => x.UserId == userId).ToListAsync();
        var userPhotos = await databaseContext.UserPhotos.Where(x => x.UserId == userId).ToListAsync();
        var userInfos = await databaseContext.UserInfo.Where(x => x.UserId == userId).ToListAsync();
        var userTokens = await databaseContext.UserTokens.Where(x => x.UserId == userId).ToListAsync();
        var userRefreshTokens = await databaseContext.UserRefreshTokens.Where(x => x.UserId == userId).ToListAsync();
        var userRoles = await databaseContext.UserRoles.Where(x => x.UserId == userId).ToListAsync();
        var userPermissions = await databaseContext.UserPermissions.Where(x => x.UserId == userId).ToListAsync();

        albums.Should().HaveCount(0);
        articles.Should().HaveCount(0);
        articleLikes.Should().HaveCount(0);
        articleCounts.Should().HaveCount(0);
        userPhotos.Should().HaveCount(0);
        userInfos.Should().HaveCount(0);
        userTokens.Should().HaveCount(0);
        userRefreshTokens.Should().HaveCount(0);
        userRoles.Should().HaveCount(0);
        userPermissions.Should().HaveCount(0);
        users.Should().BeNull();
    }
}