using Xunit;
using Moq;
using FluentAssertions;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Tests.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Storage.AzureBlobStorage;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;

namespace TokanPages.Tests.UnitTests.Handlers.Articles
{
    public class AddArticleCommandHandlerTest : TestBase
    {
        private readonly Mock<AzureBlobStorageFactory> FMockedAzureBlobStorageFactory;
        
        public AddArticleCommandHandlerTest()
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
        public async Task GivenFieldsWithBase64Image_WhenAddArticle_ShouldAddEntity() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString().ToBase64Encode()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            
            var LAddArticleCommandHandler = new AddArticleCommandHandler(
                LDatabaseContext, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

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
        public async Task GivenFieldsWithNoBase64Image_WhenAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª"
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();

            var LAddArticleCommandHandler = new AddArticleCommandHandler(
                LDatabaseContext, 
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));
        }
    }
}
