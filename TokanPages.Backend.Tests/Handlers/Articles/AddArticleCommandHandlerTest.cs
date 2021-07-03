using Xunit;
using Moq;
using FluentAssertions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Storage.AzureBlobStorage;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Shared.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Articles
{
    public class AddArticleCommandHandlerTest : TestBase
    {
        private readonly Mock<AzureBlobStorageFactory> FMockedAzureBlobStorageFactory;
        
        private readonly DataProviderService FDataProviderService;

        public AddArticleCommandHandlerTest()
        {
            FDataProviderService = new DataProviderService();
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
        public async Task GivenFieldsWithBase64ImageAsLoggedUser_WhenAddArticle_ShouldAddArticle() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString().ToBase64Encode()
            };

            var LUser = new Backend.Domain.Entities.Users
            {
                UserAlias  = FDataProviderService.GetRandomString(),
                IsActivated = true,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = FDataProviderService.GetRandomDateTime(),
                LastUpdated = FDataProviderService.GetRandomDateTime(),
                AvatarName = FDataProviderService.GetRandomString(),
                ShortBio = FDataProviderService.GetRandomString(),
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserProvider>();

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync(LUser.Id);
            
            var LAddArticleCommandHandler = new AddArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            // Act
            var LResult = await LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None);
            
            // Assert
            LResult.ToString().IsGuid().Should().Be(true);
        }
        
        [Fact]
        public async Task GivenFieldsWithBase64ImageAsAnonymousUser_WhenAddArticle_ShouldThrowError() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString().ToBase64Encode()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserProvider>();
            
            var LAddArticleCommandHandler = new AddArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenFieldsWithNoBase64ImageAsAnonymousUser_WhenAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª"
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserProvider>();

            var LAddArticleCommandHandler = new AddArticleCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedDateTime.Object, 
                FMockedAzureBlobStorageFactory.Object);
            
            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAddArticleCommandHandler.Handle(LAddArticleCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
    }
}
