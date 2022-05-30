namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Extensions;
using Backend.Domain.Entities;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Services.AzureStorageService;
using Backend.Core.Utilities.DataUtilityService;
using TokanPages.Services.AzureStorageService.Factory;

public class AddArticleCommandHandlerTest : TestBase
{
    private readonly Mock<IAzureBlobStorageFactory> _mockedAzureBlobStorageFactory;
        
    private readonly DataUtilityService _dataUtilityService;

    public AddArticleCommandHandlerTest()
    {
        _dataUtilityService = new DataUtilityService();
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
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlobStorage.Object);
    }

    [Fact]
    public async Task GivenLoggedUser_WhenAddArticle_ShouldAddArticle() 
    {
        // Arrange
        var command = new AddArticleCommand
        {
            Title = _dataUtilityService.GetRandomString(),
            Description = _dataUtilityService.GetRandomString(),
            TextToUpload = _dataUtilityService.GetRandomString(),
            ImageToUpload = _dataUtilityService.GetRandomString().ToBase64Encode()
        };

        var user = new Users
        {
            UserAlias  = _dataUtilityService.GetRandomString(),
            IsActivated = true,
            EmailAddress = _dataUtilityService.GetRandomEmail(),
            CryptedPassword = _dataUtilityService.GetRandomString()
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