using Xunit;
using Moq;
using MockQueryable.Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.UnitTests.FakeDatabase;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class AddArticleCommandHandlerTest
    {

        private readonly Mock<DatabaseContext> FDatabaseContext;

        public AddArticleCommandHandlerTest() 
        {
            FDatabaseContext = new Mock<TokanPages.Backend.Database.DatabaseContext>();
            var LArticlesDbSet = DummyLoad.GetArticles().AsQueryable().BuildMockDbSet();
            FDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);
        }

        [Fact]
        public async Task AddArticle_WhenFieldsAreProvidedWithBase64Image_ShouldExecuteSaveAsyncOnce() 
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = "Title",
                Description = "Description",
                TextToUpload = "TextToUpload",
                ImageToUpload = "+DLnpYzLUHeUfXB4LgE1mA=="
            };

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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(FDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act
            await LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None);

            // Assert
            FDatabaseContext.Verify(DbContext => DbContext.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

        [Fact]
        public async Task AddArticle_WhenFieldsAreProvidedWithNoBase64Image_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = "Title",
                Description = "Description",
                TextToUpload = "TextToUpload",
                ImageToUpload = "ImageToUpload"
            };

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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(FDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));

        }

    }

}
