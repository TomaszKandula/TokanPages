namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Storage.AzureBlobStorage;
    using Core.Utilities.DateTimeService;
    using Cqrs.Handlers.Commands.Articles;
    using Storage.AzureBlobStorage.Factory;
    using Cqrs.Services.UserServiceProvider;
    using Core.Utilities.DataUtilityService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class AddArticleCommandHandlerTest : TestBase
    {
        private readonly Mock<AzureBlobStorageFactory> FMockedAzureBlobStorageFactory;
        
        private readonly DataUtilityService FDataUtilityService;

        public AddArticleCommandHandlerTest()
        {
            FDataUtilityService = new DataUtilityService();
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
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString().ToBase64Encode()
            };

            var LUser = new Backend.Domain.Entities.Users
            {
                UserAlias  = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = FDataUtilityService.GetRandomDateTime(),
                LastUpdated = FDataUtilityService.GetRandomDateTime(),
                AvatarName = FDataUtilityService.GetRandomString(),
                ShortBio = FDataUtilityService.GetRandomString(),
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();

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
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString().ToBase64Encode()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();
            
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
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª"
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();

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