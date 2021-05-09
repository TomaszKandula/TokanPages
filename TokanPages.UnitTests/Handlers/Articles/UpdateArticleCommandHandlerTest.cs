using Xunit;
using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Storage.AzureBlobStorage;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;

namespace TokanPages.UnitTests.Handlers.Articles
{
    public class UpdateArticleCommandHandlerTest : TestBase
    {
        private const string IP_ADDRESS = "255.255.255.255";

        private readonly Mock<AzureBlobStorageFactory> FMockedAzureBlobStorageFactory;

        public UpdateArticleCommandHandlerTest()
        {
            FMockedAzureBlobStorageFactory = new Mock<AzureBlobStorageFactory>();
            var LMockedAzureBlobStorage = new Mock<IAzureBlobStorage>();

            LMockedAzureBlobStorage
                .Setup(AAzureBlobStorage => AAzureBlobStorage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            FMockedAzureBlobStorageFactory
                .Setup(AAzureBlobStorageFactory => AAzureBlobStorageFactory.Create())
                .Returns(LMockedAzureBlobStorage.Object);
        }

        [Fact]
        public async Task GivenExistingArticleAsLoggedUser_WhenUpdateArticle_ShouldUpdateEntity() 
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleCommand 
            { 
                Id = LArticles.Id,
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(150),
                ImageToUpload = StringProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = true
            };

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.Title.Should().Be(LUpdateArticleCommand.Title);
            LArticlesEntity.Description.Should().Be(LUpdateArticleCommand.Description); 
            LArticlesEntity.IsPublished.Should().BeFalse();
            LArticlesEntity.ReadCount.Should().Be(1);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(50, 25)]
        [InlineData(70, 25)]
        public async Task GivenNewLikesAddedAsAnonymousUser_WhenUpdateArticle_ShouldAddLikes(int ALikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = LArticles.Id,
                AddToLikes = ALikes,
            };

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10, 20)]
        [InlineData(50, 10, 25)]
        [InlineData(70, 10, 25)]
        public async Task GivenExistingLikesUpdatedAsAnonymousUser_WhenUpdateArticle_ShouldModifyLikes(int AExistingLikes, int ANewLikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LLikes = new Backend.Domain.Entities.ArticleLikes 
            { 
                ArticleId = LArticles.Id,
                UserId = null,
                IpAddress = IP_ADDRESS,
                LikeCount = AExistingLikes
            };

            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = LArticles.Id,
                AddToLikes = ANewLikes,
            };

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(50, 50)]
        [InlineData(70, 50)]
        public async Task GivenNewLikesAddedAsLoggedUser_WhenUpdateArticle_ShouldAddLikes(int ALikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = LArticles.Id,
                AddToLikes = ALikes,
            };

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].UserId.Should().NotBeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10, 20)]
        [InlineData(50, 10, 50)]
        [InlineData(70, 10, 50)]
        public async Task GivenExistingLikesUpdatedAsLoggedUser_WhenUpdateArticle_ShouldModifyLikes(int AExistingLikes, int ANewLikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LLikes = new Backend.Domain.Entities.ArticleLikes 
            { 
                ArticleId = LArticles.Id,
                UserId = LUsers.Id,
                IpAddress = IP_ADDRESS,
                LikeCount = AExistingLikes
            };

            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = LArticles.Id,
                AddToLikes = ANewLikes,
            };

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].UserId.Should().NotBeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }
        
        [Fact]
        public async Task GivenNotExistingArticle_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("a54aa009-2894-407f-90ad-5f07a3145203"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(150),
                ImageToUpload = StringProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => 
                LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
        }

        [Fact]
        public async Task GivenInvalidImageBase64_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("a54aa009-2894-407f-90ad-5f07a3145203"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(150),
                ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª",
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .Returns(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
        }
    }
}
