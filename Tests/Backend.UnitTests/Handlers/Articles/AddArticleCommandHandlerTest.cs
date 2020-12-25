using Xunit;
using Moq;
using FluentAssertions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class AddArticleCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task AddArticle_WhenFieldsAreProvidedWithBase64Image_ShouldAddEntity() 
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = "Title",
                Description = "Description",
                TextToUpload = "TextToUpload",
                ImageToUpload = "+DLnpYzLUHeUfXB4LgE1mA=="
            };

            var LDatabaseContext = GetTestDatabaseContext();
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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(LDatabaseContext, LMockedStorage.Object, LMockedUtility.Object);

            // Act
            await LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LArticesEntity = LAssertDbContext.Articles.ToList();
            LArticesEntity.Should().HaveCount(1);
            LArticesEntity[0].Title.Should().Be(LAddArticleCommand.Title);
            LArticesEntity[0].Description.Should().Be(LAddArticleCommand.Description);

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

            var LDatabaseContext = GetTestDatabaseContext();
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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(LDatabaseContext, LMockedStorage.Object, LMockedUtility.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));

        }

    }

}
