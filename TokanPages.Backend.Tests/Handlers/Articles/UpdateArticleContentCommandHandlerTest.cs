namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Utilities.LoggerService;
    using Domain.Entities;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Storage.AzureBlobStorage;
    using Core.Utilities.DateTimeService;
    using Cqrs.Handlers.Commands.Articles;
    using Storage.AzureBlobStorage.Factory;
    using Cqrs.Services.UserServiceProvider;

    public class UpdateArticleContentCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenExistingArticleAndLoggedUser_WhenUpdateArticle_ShouldUpdateEntity() 
        {
            // Arrange
            var userId = Guid.NewGuid();
            var users = new Users
            {
                Id = userId,
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

            var articleId = Guid.NewGuid();
            var articles = new Articles
            {
                Id = articleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = userId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.Articles.AddAsync(articles);
            await databaseContext.SaveChangesAsync();

            var mockedUserProvider = new Mock<IUserServiceProvider>();
            var mockedDateTime = new Mock<IDateTimeService>();
            var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>();
            var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
            var mockedLogger = new Mock<ILoggerService>();

            mockedUserProvider
                .Setup(provider => provider.GetUserId())
                .ReturnsAsync(users.Id);

            mockedAzureBlobStorage
                .Setup(storage => storage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            mockedAzureBlobStorageFactory
                .Setup(factory => factory.Create())
                .Returns(mockedAzureBlobStorage.Object);

            var updateArticleCommandHandler = new UpdateArticleContentCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedUserProvider.Object, 
                mockedDateTime.Object, 
                mockedAzureBlobStorageFactory.Object);

            var updateArticleCommand = new UpdateArticleContentCommand 
            { 
                Id = articleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };

            // Act
            await updateArticleCommandHandler.Handle(updateArticleCommand, CancellationToken.None);

            // Assert
            var articlesEntity = await databaseContext.Articles
                .FindAsync(updateArticleCommand.Id);

            articlesEntity.Should().NotBeNull();
            articlesEntity.Title.Should().Be(updateArticleCommand.Title);
            articlesEntity.Description.Should().Be(updateArticleCommand.Description); 
            articlesEntity.IsPublished.Should().BeFalse();
        }

        [Fact]
        public async Task GivenNonExistingArticleAndLoggedUser_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var users = new Users
            {
                Id = userId,
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

            var articleId = Guid.NewGuid();
            var articles = new Articles
            {
                Id = articleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = userId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.Articles.AddAsync(articles);
            await databaseContext.SaveChangesAsync();

            var mockedUserProvider = new Mock<IUserServiceProvider>();
            var mockedDateTime = new Mock<IDateTimeService>();
            var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>();
            var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
            var mockedLogger = new Mock<ILoggerService>();
            
            mockedUserProvider
                .Setup(provider => provider.GetUserId())
                .ReturnsAsync(users.Id);

            mockedAzureBlobStorage
                .Setup(storage => storage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            mockedAzureBlobStorageFactory
                .Setup(factory => factory.Create())
                .Returns(mockedAzureBlobStorage.Object);

            var updateArticleCommandHandler = new UpdateArticleContentCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedUserProvider.Object, 
                mockedDateTime.Object, 
                mockedAzureBlobStorageFactory.Object);

            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => 
                updateArticleCommandHandler.Handle(updateArticleCommand, CancellationToken.None));

            result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
        }

        [Fact]
        public async Task GivenExistingArticleAndAnonymousUser_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var articleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var updateArticleContentCommand = new UpdateArticleContentCommand
            {
                Id = articleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };

            var users = new Users
            {
                Id = userId,
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

            var articles = new Articles
            {
                Id = articleId,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = userId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.Articles.AddAsync(articles);
            await databaseContext.SaveChangesAsync();

            var mockedUserProvider = new Mock<IUserServiceProvider>();
            var mockedDateTime = new Mock<IDateTimeService>();
            var mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>(); 
            var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();
            var mockedLogger = new Mock<ILoggerService>();

            mockedUserProvider
                .Setup(provider => provider.GetUserId())
                .ReturnsAsync((Guid?) null);

            mockedAzureBlobStorage
                .Setup(storage => storage.UploadFile(
                    It.IsAny<Stream>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            
            mockedAzureBlobStorageFactory
                .Setup(factory => factory.Create())
                .Returns(mockedAzureBlobStorage.Object);

            var updateArticleCommandHandler = new UpdateArticleContentCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedUserProvider.Object, 
                mockedDateTime.Object, 
                mockedAzureBlobStorageFactory.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => updateArticleCommandHandler.Handle(updateArticleContentCommand, CancellationToken.None));

            result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
    }
}