using Xunit;
using Moq;
using MockQueryable.Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.UnitTests.FakeDatabase;
using TokanPages.Backend.Database;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Core.Services.FileUtility;
using System.Threading;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;

namespace Backend.UnitTests.Handlers.Articles
{

    public class UpdateArticleCommandHandlerTest
    {

        private readonly Mock<DatabaseContext> FDatabaseContext;

        public UpdateArticleCommandHandlerTest()
        {
            FDatabaseContext = new Mock<DatabaseContext>();
            var LArticlesDbSet = DummyLoad.GetArticles().AsQueryable().BuildMockDbSet();
            FDatabaseContext.Setup(DatabaseContext => DatabaseContext.Articles).Returns(LArticlesDbSet.Object);
        }

        [Fact]
        public async Task UpdateArticle_WhenArticleExists_ShouldUpdate() 
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand 
            { 
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = "",
                Description = "",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();

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

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(FDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act
            await LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            FDatabaseContext.Verify(DbContext => DbContext.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

        [Fact]
        public async Task UpdateArticle_WhenArticleNotExists_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Parse("a54aa009-2894-407f-90ad-5f07a3145203"),
                Title = "",
                Description = "",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtility>();

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

            var LUpdateArticleCommandHandler = new UpdateArticleCommandHandler(FDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateArticleCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));

        }

    }

}
