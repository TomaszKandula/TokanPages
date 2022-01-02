namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using Backend.Core.Extensions;
using Backend.Domain.Entities;
using Backend.Shared.Resources;
using Backend.Storage.AzureBlobStorage;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Handlers.Commands.Articles;
using Backend.Storage.AzureBlobStorage.Factory;
using Backend.Cqrs.Services.UserServiceProvider;
using Backend.Core.Utilities.DataUtilityService;

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
                It.IsAny<CancellationToken>(),
                It.IsAny<string>()))
            .Returns(Task.FromResult(string.Empty));
            
        _mockedAzureBlobStorageFactory
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlobStorage.Object);
    }

    [Fact]
    public async Task GivenFieldsWithBase64ImageAsLoggedUser_WhenAddArticle_ShouldAddArticle() 
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
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
            FirstName = _dataUtilityService.GetRandomString(),
            LastName = _dataUtilityService.GetRandomString(),
            EmailAddress = _dataUtilityService.GetRandomEmail(),
            Registered = _dataUtilityService.GetRandomDateTime(),
            LastLogged = _dataUtilityService.GetRandomDateTime(),
            LastUpdated = _dataUtilityService.GetRandomDateTime(),
            AvatarName = _dataUtilityService.GetRandomString(),
            ShortBio = _dataUtilityService.GetRandomString(),
            CryptedPassword = _dataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserProvider
            .Setup(provider => provider.GetUserId())
            .ReturnsAsync(user.Id);
            
        var addArticleCommandHandler = new AddArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedDateTime.Object, 
            _mockedAzureBlobStorageFactory.Object);

        // Act
        var result = await addArticleCommandHandler.Handle(addArticleCommand, CancellationToken.None);
            
        // Assert
        result.ToString().IsGuid().Should().Be(true);
    }
        
    [Fact]
    public async Task GivenFieldsWithBase64ImageAsAnonymousUser_WhenAddArticle_ShouldThrowError() 
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = _dataUtilityService.GetRandomString(),
            Description = _dataUtilityService.GetRandomString(),
            TextToUpload = _dataUtilityService.GetRandomString(),
            ImageToUpload = _dataUtilityService.GetRandomString().ToBase64Encode()
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();
            
        var addArticleCommandHandler = new AddArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedDateTime.Object, 
            _mockedAzureBlobStorageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() 
            => addArticleCommandHandler.Handle(addArticleCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
        
    [Fact]
    public async Task GivenFieldsWithNoBase64ImageAsAnonymousUser_WhenAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = _dataUtilityService.GetRandomString(),
            Description = _dataUtilityService.GetRandomString(),
            TextToUpload = _dataUtilityService.GetRandomString(),
            ImageToUpload = "úK¼Æ½t$bþÍs*L2ÕÊª"
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedLogger = new Mock<ILoggerService>();

        var addArticleCommandHandler = new AddArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedDateTime.Object, 
            _mockedAzureBlobStorageFactory.Object);
            
        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() 
            => addArticleCommandHandler.Handle(addArticleCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
}