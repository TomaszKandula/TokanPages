namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using Cqrs.Services.UserServiceProvider;
    using UsersEntity = Domain.Entities.Users;
    using ArticlesEntity = Domain.Entities.Articles;
    using PermissionsEntity = Domain.Entities.Permissions;
    using AuthorizationPermissions = Identity.Authorization.Permissions;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class UpdateArticleVisibilityCommandHandlerTest : TestBase
    {
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public async Task GivenCorrectPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldFinishSuccess(bool AIsVisible, bool AShouldBeVisible)
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = GetUser(LUserId);
            var LPermission = GetPermission();
            var LUserPermission = GetUserPermission(LUserId, LPermission.Id);
            var LArticles = GetUserArticle(LUserId, AIsVisible);

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Permissions.AddAsync(LPermission);
            await LDatabaseContext.UserPermissions.AddAsync(LUserPermission);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.HasPermissionAssigned(It.IsAny<string>()))
                .ReturnsAsync(true);

            var LUpdateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = LArticles.Id,
                IsPublished = AShouldBeVisible
            };

            var LUpdateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
                LDatabaseContext, LMockedUserProvider.Object);
            
            // Act
            await LUpdateArticleVisibilityCommandHandler.Handle(LUpdateArticleVisibilityCommand, CancellationToken.None);

            // Assert
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LArticles.Id);

            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.IsPublished.Should().Be(AShouldBeVisible);
        }

        [Fact]
        public async Task GivenNoPermissionAndExistingArticle_WhenInvokeArticleVisibility_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = GetUser(LUserId);
            var LPermission = GetPermission();
            var LUserPermission = GetUserPermission(LUserId, LPermission.Id);
            var LArticles = GetUserArticle(LUserId, false);

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Permissions.AddAsync(LPermission);
            await LDatabaseContext.UserPermissions.AddAsync(LUserPermission);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.HasPermissionAssigned(It.IsAny<string>()))
                .ReturnsAsync(false);

            var LUpdateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = LArticles.Id,
                IsPublished = true
            };

            var LUpdateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
                LDatabaseContext, LMockedUserProvider.Object);
            
            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => 
                LUpdateArticleVisibilityCommandHandler.Handle(LUpdateArticleVisibilityCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenCorrectPermissionAndWrongArticleId_WhenInvokeArticleVisibility_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = GetUser(LUserId);
            var LPermission = GetPermission();
            var LUserPermission = GetUserPermission(LUserId, LPermission.Id);
            var LArticles = GetUserArticle(LUserId, false);

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Permissions.AddAsync(LPermission);
            await LDatabaseContext.UserPermissions.AddAsync(LUserPermission);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.HasPermissionAssigned(It.IsAny<string>()))
                .ReturnsAsync(true);

            var LUpdateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };

            var LUpdateArticleVisibilityCommandHandler = new UpdateArticleVisibilityCommandHandler(
                LDatabaseContext, LMockedUserProvider.Object);
            
            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => 
                LUpdateArticleVisibilityCommandHandler.Handle(LUpdateArticleVisibilityCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
        }
        
        private ArticlesEntity GetUserArticle(Guid AUserId, bool AIsPublished)
        {
            return new ()
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = AIsPublished,
                ReadCount = 0,
                CreatedAt = DataUtilityService.GetRandomDateTime(),
                UpdatedAt = null,
                UserId = AUserId
            };
        }

        private UsersEntity GetUser(Guid AUserId)
        {
            return new()
            {
                Id = AUserId,
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
        }

        private static PermissionsEntity GetPermission()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = AuthorizationPermissions.CanPublishArticles.ToString()
            };
        }

        private static UserPermissions GetUserPermission(Guid AUserId, Guid APermissionId)
        {
            return new()
            {
                UserId = AUserId,
                PermissionId = APermissionId
            };
        }
    }
}