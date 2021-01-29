using Xunit;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Cqrs.Services.UserProvider;
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
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString(255)),
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

            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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
                LMockedUserProvider.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LArticesEntity = LAssertDbContext.Articles.Find(LUpdateArticleCommand.Id);

            LArticesEntity.Should().NotBeNull();
            LArticesEntity.Title.Should().Be(LUpdateArticleCommand.Title);
            LArticesEntity.Description.Should().Be(LUpdateArticleCommand.Description); 
            LArticesEntity.IsPublished.Should().BeFalse();
            LArticesEntity.ReadCount.Should().Be(1);
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

            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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

            var LIpAddress = "255.255.255.255";
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(LIpAddress);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.Likes.Where(x => x.ArticleId == LArticles.Id).ToList();
            var LArticesEntity = LAssertDbContext.Articles.Find(LUpdateArticleCommand.Id);

            LArticesEntity.Should().NotBeNull();
            var LikesLimitForAnonym = 25;

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(LIpAddress);
            LLikesEntity[0].UserId.Should().BeNull();
            if (ALikes == 10) 
                LLikesEntity[0].LikeCount.Should().Be(10);
            if (ALikes == 50)
                LLikesEntity[0].LikeCount.Should().Be(LikesLimitForAnonym);
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

            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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

            var LIpAddress = "255.255.255.255";
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(LIpAddress);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUserId);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.Likes.Where(x => x.ArticleId == LArticles.Id).ToList();
            var LArticesEntity = LAssertDbContext.Articles.Find(LUpdateArticleCommand.Id);

            LArticesEntity.Should().NotBeNull();
            var LikesLimitForUser = 50;

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(LIpAddress);
            LLikesEntity[0].UserId.Should().Be(LUserId);
            if (ALikes == 10)
                LLikesEntity[0].LikeCount.Should().Be(10);
            if (ALikes == 70)
                LLikesEntity[0].LikeCount.Should().Be(LikesLimitForUser);
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

            var LIpAddress = "255.255.255.255";
            var LExistingLikes = 10;
            var LLikes = new TokanPages.Backend.Domain.Entities.Likes 
            { 
                Id = Guid.NewGuid(),
                ArticleId = LArticleId,
                UserId = null,
                IpAddress = LIpAddress,
                LikeCount = LExistingLikes
            };

            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.Likes.Add(LLikes);
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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
                .Returns(LIpAddress);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.Likes.Where(x => x.ArticleId == LArticles.Id).ToList();
            var LArticesEntity = LAssertDbContext.Articles.Find(LUpdateArticleCommand.Id);

            LArticesEntity.Should().NotBeNull();
            var LikesLimitForAnonym = 25;

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(LIpAddress);
            LLikesEntity[0].UserId.Should().BeNull();
            if (ALikes == 10)
                LLikesEntity[0].LikeCount.Should().Be(LExistingLikes + ALikes);
            if (ALikes == 50)
                LLikesEntity[0].LikeCount.Should().Be(LikesLimitForAnonym);
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

            var LIpAddress = "255.255.255.255";
            var LUserId = Guid.Parse("c5ac0f04-6346-4676-a82b-0710099d08f6");
            var LExistingLikes = 10;
            var LLikes = new TokanPages.Backend.Domain.Entities.Likes
            {
                Id = Guid.NewGuid(),
                ArticleId = LArticleId,
                UserId = LUserId,
                IpAddress = LIpAddress,
                LikeCount = LExistingLikes
            };

            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.Likes.Add(LLikes);
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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
                .Returns(LIpAddress);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUserId);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext,
                LMockedStorage.Object,
                LMockedUtility.Object,
                LMockedUserProvider.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LLikesEntity = LAssertDbContext.Likes.Where(x => x.ArticleId == LArticles.Id).ToList();
            var LArticesEntity = LAssertDbContext.Articles.Find(LUpdateArticleCommand.Id);

            LArticesEntity.Should().NotBeNull();
            var LikesLimitForUser = 50;

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(LIpAddress);
            LLikesEntity[0].UserId.Should().Be(LUserId);
            if (ALikes == 10)
                LLikesEntity[0].LikeCount.Should().Be(LExistingLikes + ALikes);
            if (ALikes == 50)
                LLikesEntity[0].LikeCount.Should().Be(LikesLimitForUser);
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
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString(255)),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LDatabaseContext = GetTestDatabaseContext();
            LDatabaseContext.Articles.Add(new TokanPages.Backend.Domain.Entities.Articles
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
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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
                LMockedUserProvider.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
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
                ImageToUpload = DataProvider.GetRandomString(255),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LDatabaseContext = GetTestDatabaseContext();
            LDatabaseContext.Articles.Add(new TokanPages.Backend.Domain.Entities.Articles
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
            LDatabaseContext.SaveChanges();

            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();
            var LMockedUserProvider = new Mock<UserProvider>();

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
                LMockedUserProvider.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
        }
    }
}
