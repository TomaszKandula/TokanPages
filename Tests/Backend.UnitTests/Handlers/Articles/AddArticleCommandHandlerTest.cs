using Xunit;
using Moq;
using FluentAssertions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
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
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString())
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();

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
            var LArticlesEntity = LAssertDbContext.Articles.ToList();
            LArticlesEntity.Should().HaveCount(1);
            LArticlesEntity[0].Title.Should().Be(LAddArticleCommand.Title);
            LArticlesEntity[0].Description.Should().Be(LAddArticleCommand.Description);
        }

        [Fact]
        public async Task AddArticle_WhenFieldsAreProvidedWithNoBase64Image_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString(3)
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedStorage = new Mock<AzureStorageService>();
            var LMockedUtility = new Mock<FileUtilityService>();

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
            await Assert.ThrowsAsync<BusinessException>(() 
                => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));
        }
    }
}
