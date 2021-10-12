namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Core.Exceptions;
    using Shared.Resources;
    using Domain.Entities;
    using Cqrs.Handlers.Commands.Articles;
    using Cqrs.Services.UserServiceProvider;

    public class UpdateArticleCountCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenLoggedUserAndExistingArticleAndNoReads_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = new Users
            {
                Id = LUserId,
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticleId = Guid.NewGuid();
            var LArticles = new Articles
            {
                Id = LArticleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LExpectedTotalReadCount = LArticles.ReadCount + 1;
            var LMockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AService => AService.GetUserId())
                .ReturnsAsync(LUserId);

            LMockedUserServiceProvider
                .Setup(AService => AService.GetRequestIpAddress())
                .Returns(LMockedIpAddress);

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext, LMockedUserServiceProvider.Object);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = LArticleId };

            // Act
            await LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            var LArticleCounts = await LDatabaseContext.ArticleCounts
                .Where(ACounts => ACounts.ArticleId == LArticleId)
                .ToListAsync();

            // Assert
            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.ReadCount.Should().Be(LExpectedTotalReadCount);
            LArticleCounts.Should().HaveCount(1);
            LArticleCounts.Select(ACounts => ACounts.ReadCount).First().Should().Be(1);
        }

        [Fact]
        public async Task GivenLoggedUserAndExistingArticleAndExistingReads_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = new Users
            {
                Id = LUserId,
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticleId = Guid.NewGuid();
            var LArticles = new Articles
            {
                Id = LArticleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            var LMockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LArticlesCounts = new ArticleCounts
            {
                UserId = LUserId,
                ArticleId = LArticleId,
                IpAddress = LMockedIpAddress,
                ReadCount = 5
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.ArticleCounts.AddAsync(LArticlesCounts);
            await LDatabaseContext.SaveChangesAsync();

            var LExpectedTotalReadCount = LArticles.ReadCount + 1;
            var LExpectedUserReadCount = LArticlesCounts.ReadCount + 1;
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AService => AService.GetUserId())
                .ReturnsAsync(LUserId);

            LMockedUserServiceProvider
                .Setup(AService => AService.GetRequestIpAddress())
                .Returns(LMockedIpAddress);

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext, LMockedUserServiceProvider.Object);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = LArticles.Id };

            // Act
            await LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            var LArticleCounts = await LDatabaseContext.ArticleCounts
                .Where(ACounts => ACounts.ArticleId == LArticles.Id)
                .ToListAsync();
            
            // Assert
            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.ReadCount.Should().Be(LExpectedTotalReadCount);
            LArticleCounts.Should().HaveCount(1);
            LArticleCounts.Select(ACounts => ACounts.ReadCount).First().Should().Be(LExpectedUserReadCount);
        }

        [Fact]
        public async Task GivenAnonymousUserAndExistingArticle_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = new Users
            {
                Id = LUserId,
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticleId = Guid.NewGuid();
            var LArticles = new Articles
            {
                Id = LArticleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LExpectedTotalReadCount = LArticles.ReadCount + 1;
            var LMockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AService => AService.GetUserId())
                .ReturnsAsync((Guid?)null);

            LMockedUserServiceProvider
                .Setup(AService => AService.GetRequestIpAddress())
                .Returns(LMockedIpAddress);

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext, LMockedUserServiceProvider.Object);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = LArticleId };

            // Act
            await LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            var LArticleCounts = await LDatabaseContext.ArticleCounts
                .Where(ACounts => ACounts.ArticleId == LArticleId)
                .ToListAsync();

            // Assert
            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.ReadCount.Should().Be(LExpectedTotalReadCount);
            LArticleCounts.Should().HaveCount(0);
        }

        [Fact]
        public async Task GivenExistingArticleAndIncorrectArticleId_WhenUpdateArticleCount_ShouldThrowError()
        {
            // Arrange
            var LUsers = new Users
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new Articles
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AService => AService.GetUserId())
                .ReturnsAsync((Guid?)null);

            LMockedUserServiceProvider
                .Setup(AService => AService.GetRequestIpAddress())
                .Returns(LMockedIpAddress);

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext, LMockedUserServiceProvider.Object);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = Guid.NewGuid() };

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
        }
    }
}