using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class AddArticleCommandHandlerTest : TestBase
{
    private readonly Mock<IAzureBlobStorageFactory> _mockedAzureBlobStorageFactory;

    public AddArticleCommandHandlerTest()
    {
        _mockedAzureBlobStorageFactory = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlobStorage = new Mock<IAzureBlobStorage>();

        mockedAzureBlobStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(string.Empty));

        _mockedAzureBlobStorageFactory
            .Setup(factory => factory.Create(It.IsAny<ILoggerService>()))
            .Returns(mockedAzureBlobStorage.Object);
    }

    [Fact]
    public async Task GivenLoggedUser_WhenAddArticle_ShouldAddArticle() 
    {
        // Arrange
        var command = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString().ToBase64Encode()
        };

        var user = new Backend.Domain.Entities.User.Users
        {
            UserAlias  = DataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new AddArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object,
            mockedDateTime.Object, 
            _mockedAzureBlobStorageFactory.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ToString().IsGuid().Should().Be(true);
    }
}