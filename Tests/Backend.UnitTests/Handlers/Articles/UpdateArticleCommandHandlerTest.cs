using Xunit;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Storage.AzureStorage;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using Backend.TestData;

namespace Backend.UnitTests.Handlers.Articles
{
    public class UpdateArticleCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task UpdateArticle_WhenArticleExists_ShouldUpdateEntity() 
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand 
            { 
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = DataProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = true
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns("255.255.255.255");

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedStorage.Object, 
                LMockedUtility.Object, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LArticlesEntity = await LAssertDbContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.Title.Should().Be(LUpdateArticleCommand.Title);
            LArticlesEntity.Description.Should().Be(LUpdateArticleCommand.Description); 
            LArticlesEntity.IsPublished.Should().BeFalse();
            LArticlesEntity.ReadCount.Should().Be(1);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        public async Task UpdateArticle_WhenNewLikesAddedAsAnonymous_ShouldAddLikes(int ALikes)
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                AddToLikes = ALikes,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            const string IP_ADDRESS = "255.255.255.255";
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object,
                LMockedDateTime.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();

            var LArticlesEntity = await LAssertDbContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();

            switch (ALikes)
            {
                case 10:
                    LLikesEntity[0].LikeCount.Should().Be(10);
                    break;
                case 50:
                    LLikesEntity[0].LikeCount.Should().Be(Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS);
                    break;
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(70)]
        public async Task UpdateArticle_WhenNewLikesAddedAsUser_ShouldAddLikes(int ALikes)
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                AddToLikes = ALikes,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUserId = Guid.Parse("c5ac0f04-6346-4676-a82b-0710099d08f6");
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            const string IP_ADDRESS = "255.255.255.255";
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUserId);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object, 
                LMockedDateTime.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LAssertDbContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().Be(LUserId);
            
            switch (ALikes)
            {
                case 10:
                    LLikesEntity[0].LikeCount.Should().Be(10);
                    break;
                case 70:
                    LLikesEntity[0].LikeCount.Should().Be(Constants.Likes.LIKES_LIMIT_FOR_USER);
                    break;
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        public async Task UpdateArticle_WhenExistingLikesUpdatedAsAnonymous_ShouldModifyLikes(int ALikes)
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                AddToLikes = ALikes,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LArticleId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            };

            const string IP_ADDRESS = "255.255.255.255";
            const int EXISTING_LIKES = 10;
            
            var LLikes = new TokanPages.Backend.Domain.Entities.ArticleLikes 
            { 
                Id = Guid.NewGuid(),
                ArticleId = LArticleId,
                UserId = null,
                IpAddress = IP_ADDRESS,
                LikeCount = EXISTING_LIKES
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object,
                LMockedDateTime.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LAssertDbContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();
            
            switch (ALikes)
            {
                case 10:
                    LLikesEntity[0].LikeCount.Should().Be(EXISTING_LIKES + ALikes);
                    break;
                case 50:
                    LLikesEntity[0].LikeCount.Should().Be(Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS);
                    break;
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        public async Task UpdateArticle_WhenExistingLikesUpdatedAsUser_ShouldModifyLikes(int ALikes)
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                AddToLikes = ALikes,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LArticleId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            };

            const string IP_ADDRESS = "255.255.255.255";
            const int EXISTING_LIKES = 10;

            var LUserId = Guid.Parse("c5ac0f04-6346-4676-a82b-0710099d08f6");
            
            var LLikes = new TokanPages.Backend.Domain.Entities.ArticleLikes
            {
                Id = Guid.NewGuid(),
                ArticleId = LArticleId,
                UserId = LUserId,
                IpAddress = IP_ADDRESS,
                LikeCount = EXISTING_LIKES
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUserId);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object, 
                LMockedDateTime.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LAssertDbContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().Be(LUserId);
            
            switch (ALikes)
            {
                case 10:
                    LLikesEntity[0].LikeCount.Should().Be(EXISTING_LIKES + ALikes);
                    break;
                case 50:
                    LLikesEntity[0].LikeCount.Should().Be(Constants.Likes.LIKES_LIMIT_FOR_USER);
                    break;
            }
        }

        [Fact]
        public async Task UpdateArticle_WhenArticleNotExists_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("a54aa009-2894-407f-90ad-5f07a3145203"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = DataProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Articles.AddAsync(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            });
            
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns("255.255.255.255");

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedStorage.Object, 
                LMockedUtility.Object, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => 
                LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateArticle_WhenImageBase64IsInvalid_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("a54aa009-2894-407f-90ad-5f07a3145203"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª",
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Articles.AddAsync(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            });
            
            await LDatabaseContext.SaveChangesAsync();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();
            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUtility.Setup(AMockedUtility => AMockedUtility.SaveToFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            LMockedStorage.Setup(AMockedStorage => AMockedStorage.UploadFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new ActionResult { IsSucceeded = true }));

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns("255.255.255.255");

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedStorage.Object, 
                LMockedUtility.Object, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
        }
    }
}
