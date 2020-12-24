using Xunit;
using Moq;
using MockQueryable.Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using DatabaseContext = TokanPages.Backend.Database.DatabaseContext;

namespace Backend.UnitTests.Handlers.Articles
{

    public class AddArticleCommandHandlerTest
    {

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

            var LDatabaseContext = new Mock<DatabaseContext>();
            var LDummyLoad = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Title = "Why C# is great?",
                    Description = "More on C#",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Title = "NET Core 5 is coming",
                    Description = "What's new?",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };

            var LArticlesDbSet = LDummyLoad.AsQueryable().BuildMockDbSet();
            LDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);

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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(LDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act
            await LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None);

            // Assert
            LDatabaseContext.Verify(DbContext => DbContext.SaveChangesAsync(CancellationToken.None), Times.Once);

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

            var LDatabaseContext = new Mock<DatabaseContext>();
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

            var LAddArticleCommandHandler = new AddArticleCommandHandler(LDatabaseContext.Object, LMockedStorage.Object, LMockedUtility.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));

        }

    }

}
