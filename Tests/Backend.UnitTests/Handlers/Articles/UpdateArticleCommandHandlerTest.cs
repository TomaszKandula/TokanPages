using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Services.UserProvider;
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
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString(255)),
                IsPublished = false,
                AddToLikes = 100,
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
                UpdatedAt = null
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
                UpdatedAt = null
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
                UpdatedAt = null
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
