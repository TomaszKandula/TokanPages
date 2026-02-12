using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class RemoveUserCommandHandlerTest : TestBase
{
    // TODO: redo
    // [Fact]
    // public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
    // {
    //     // Arrange
    //     var userId = Guid.NewGuid();
    //     var user = new User
    //     {
    //         Id = userId,
    //         EmailAddress = DataUtilityService.GetRandomEmail(),
    //         UserAlias = DataUtilityService.GetRandomString(),
    //         IsActivated = true,
    //         CryptedPassword = DataUtilityService.GetRandomString(),
    //         ResetId = null,
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default,
    //         IsVerified = false,
    //         IsDeleted = false,
    //         HasBusinessLock = false
    //     };
    //
    //     var role = new Role
    //     {
    //         Id = Guid.NewGuid(),
    //         Name = DataUtilityService.GetRandomString(),
    //         Description = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var permission = new Permission
    //     {
    //         Id = Guid.NewGuid(),
    //         Name = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default,
    //     };
    //
    //     var photoGear = new PhotoGear
    //     {
    //         Id = Guid.NewGuid(),
    //         Aperture = DataUtilityService.GetRandomDecimal(),
    //         BodyModel = DataUtilityService.GetRandomString(),
    //         BodyVendor = DataUtilityService.GetRandomString(),
    //         FilmIso = DataUtilityService.GetRandomInteger(),
    //         FocalLength = DataUtilityService.GetRandomInteger(),
    //         LensName = DataUtilityService.GetRandomString(),
    //         LensVendor = DataUtilityService.GetRandomString(),
    //         ShutterSpeed = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var photoCategory = new PhotoCategory
    //     {
    //         Id = Guid.NewGuid(),
    //         CategoryName = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //     
    //     var userPhoto = new Photo
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         PhotoGearId = photoGear.Id,
    //         PhotoCategoryId = photoCategory.Id,
    //         Keywords = DataUtilityService.GetRandomString(),
    //         PhotoUrl = DataUtilityService.GetRandomString(),
    //         DateTaken = DataUtilityService.GetRandomDateTime(),
    //         Title = DataUtilityService.GetRandomString(),
    //         Description = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var userInfo = new UserInfo
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         FirstName = DataUtilityService.GetRandomString(),
    //         LastName = DataUtilityService.GetRandomString(),
    //         UserAboutText = DataUtilityService.GetRandomString(),
    //         UserImageName = DataUtilityService.GetRandomString(),
    //         UserVideoName = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         ModifiedBy = null,
    //         ModifiedAt = null,
    //     };
    //
    //     var userToken = new UserToken
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         Token = DataUtilityService.GetRandomString(),
    //         Expires = DataUtilityService.GetRandomDateTime(),
    //         Created = DataUtilityService.GetRandomDateTime(),
    //         CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
    //         Command = DataUtilityService.GetRandomString(),
    //         Revoked = null,
    //         RevokedByIp = string.Empty,
    //         ReasonRevoked = string.Empty,
    //     };
    //
    //     var userRefreshToken = new UserRefreshToken
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         Token = DataUtilityService.GetRandomString(),
    //         Expires = DataUtilityService.GetRandomDateTime(),
    //         Created = DataUtilityService.GetRandomDateTime(),
    //         CreatedByIp = DataUtilityService.GetRandomIpAddress()
    //             .ToString(),
    //         Revoked = null,
    //         RevokedByIp = string.Empty,
    //         ReasonRevoked = string.Empty,
    //         ReplacedByToken = string.Empty,
    //     };
    //
    //     var userRole = new UserRole
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         RoleId = role.Id,
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var userPermission = new UserPermission
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         PermissionId = permission.Id,
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var album = new Album
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         UserPhotoId = userPhoto.Id,
    //         Title = DataUtilityService.GetRandomString(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var article = new Article
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         Title = DataUtilityService.GetRandomString(),
    //         Description = DataUtilityService.GetRandomString(),
    //         IsPublished = true,
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         UpdatedAt = null,
    //         LanguageIso = "ENG",
    //         CreatedBy = Guid.NewGuid()
    //     };
    //
    //     var articleLike = new ArticleLike
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         ArticleId = article.Id,
    //         IpAddress = DataUtilityService.GetRandomIpAddress()
    //             .ToString(),
    //         LikeCount = DataUtilityService.GetRandomInteger(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var articleCount = new ArticleCount
    //     {
    //         Id = Guid.NewGuid(),
    //         UserId = userId,
    //         ArticleId = article.Id,
    //         IpAddress = DataUtilityService.GetRandomIpAddress()
    //             .ToString(),
    //         ReadCount = DataUtilityService.GetRandomInteger(),
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Roles.AddAsync(role);
    //     await databaseContext.Permissions.AddAsync(permission);
    //     await databaseContext.PhotoGears.AddAsync(photoGear);
    //     await databaseContext.PhotoCategories.AddAsync(photoCategory);
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.Photos.AddAsync(userPhoto);
    //     await databaseContext.UserInformation.AddAsync(userInfo);
    //     await databaseContext.UserTokens.AddAsync(userToken);
    //     await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
    //     await databaseContext.UserRoles.AddAsync(userRole);
    //     await databaseContext.UserPermissions.AddAsync(userPermission);
    //     await databaseContext.Albums.AddAsync(album);
    //     await databaseContext.Articles.AddAsync(article);
    //     await databaseContext.ArticleLikes.AddAsync(articleLike);
    //     await databaseContext.ArticleCounts.AddAsync(articleCount);
    //     await databaseContext.SaveChangesAsync();
    //
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedUserService = new Mock<IUserService>();
    //
    //     mockedUserService
    //         .Setup(service => service.GetActiveUser(It.IsAny<Guid?>()))
    //         .ReturnsAsync(user);
    //
    //     var command = new RemoveUserCommand { Id = userId };
    //     var handler = new RemoveUserCommandHandler(
    //         databaseContext, 
    //         mockedLogger.Object, 
    //         mockedUserService.Object);
    //
    //     // Act
    //     await handler.Handle(command, CancellationToken.None);
    //
    //     // Assert
    //     var users = await databaseContext.Users.FindAsync(userId);
    //     var albums = await databaseContext.Albums.Where(x => x.UserId == userId).ToListAsync();
    //     var articles = await databaseContext.Articles.Where(x => x.UserId == userId).ToListAsync();
    //     var articleLikes = await databaseContext.ArticleLikes.Where(x => x.UserId == userId).ToListAsync();
    //     var articleCounts = await databaseContext.ArticleCounts.Where(x => x.UserId == userId).ToListAsync();
    //     var userPhotos = await databaseContext.Photos.Where(x => x.UserId == userId).ToListAsync();
    //     var userInfos = await databaseContext.UserInformation.Where(x => x.UserId == userId).ToListAsync();
    //     var userTokens = await databaseContext.UserTokens.Where(x => x.UserId == userId).ToListAsync();
    //     var userRefreshTokens = await databaseContext.UserRefreshTokens.Where(x => x.UserId == userId).ToListAsync();
    //     var userRoles = await databaseContext.UserRoles.Where(x => x.UserId == userId).ToListAsync();
    //     var userPermissions = await databaseContext.UserPermissions.Where(x => x.UserId == userId).ToListAsync();
    //
    //     albums.Should().HaveCount(0);
    //     articles.Should().HaveCount(0);
    //     articleLikes.Should().HaveCount(0);
    //     articleCounts.Should().HaveCount(0);
    //     userPhotos.Should().HaveCount(0);
    //     userInfos.Should().HaveCount(0);
    //     userTokens.Should().HaveCount(0);
    //     userRefreshTokens.Should().HaveCount(0);
    //     userRoles.Should().HaveCount(0);
    //     userPermissions.Should().HaveCount(0);
    //     users.Should().BeNull();
    // }
}