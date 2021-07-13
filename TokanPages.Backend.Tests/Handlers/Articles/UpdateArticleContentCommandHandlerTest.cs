namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Storage.AzureBlobStorage;
    using Shared.Services.DateTimeService;
    using Cqrs.Handlers.Commands.Articles;
    using Storage.AzureBlobStorage.Factory;
    using Cqrs.Services.UserServiceProvider;
    using Shared.Services.DataProviderService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class UpdateArticleContentCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateArticleContentCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenExistingArticleAndLoggedUser_WhenUpdateArticle_ShouldUpdateEntity() 
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = LUserId,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LArticleId = Guid.NewGuid();
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedAzureBlobStorageFactory = new Mock<AzureBlobStorageFactory>();
            var LMockedAzureBlobStorage = new Mock<IAzureBlobStorage>();

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync(LUsers.Id);

            LMockedAzureBlobStorage
                .Setup(AAzureBlobStorage => AAzureBlobStorage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            LMockedAzureBlobStorageFactory
                .Setup(AAzureBlobStorageFactory => AAzureBlobStorageFactory.Create())
                .Returns(LMockedAzureBlobStorage.Object);

            var LUpdateArticleCommandHandler = new UpdateArticleContentCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                LMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleContentCommand 
            { 
                Id = LArticleId,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode()
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
        }

        [Fact]
        public async Task GivenNonExistingArticleAndLoggedUser_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = LUserId,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LArticleId = Guid.NewGuid();
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedAzureBlobStorageFactory = new Mock<AzureBlobStorageFactory>();
            var LMockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
            
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync(LUsers.Id);

            LMockedAzureBlobStorage
                .Setup(AAzureBlobStorage => AAzureBlobStorage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            LMockedAzureBlobStorageFactory
                .Setup(AAzureBlobStorageFactory => AAzureBlobStorageFactory.Create())
                .Returns(LMockedAzureBlobStorage.Object);

            var LUpdateArticleCommandHandler = new UpdateArticleContentCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                LMockedAzureBlobStorageFactory.Object);

            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode()
            };

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => 
                LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
        }

        [Fact]
        public async Task GivenExistingArticleAndAnonymousUser_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LArticleId = Guid.NewGuid();
            var LUserId = Guid.NewGuid();
            var LUpdateArticleContentCommand = new UpdateArticleContentCommand
            {
                Id = LArticleId,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode()
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = LUserId,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUserId
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedAzureBlobStorageFactory = new Mock<AzureBlobStorageFactory>(); 
            var LMockedAzureBlobStorage = new Mock<IAzureBlobStorage>();

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync((Guid?) null);

            LMockedAzureBlobStorage
                .Setup(AAzureBlobStorage => AAzureBlobStorage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            LMockedAzureBlobStorageFactory
                .Setup(AAzureBlobStorageFactory => AAzureBlobStorageFactory.Create())
                .Returns(LMockedAzureBlobStorage.Object);

            var LUpdateArticleContentCommandHandler = new UpdateArticleContentCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object, 
                LMockedDateTime.Object, 
                LMockedAzureBlobStorageFactory.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateArticleContentCommandHandler.Handle(LUpdateArticleContentCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
    }
}