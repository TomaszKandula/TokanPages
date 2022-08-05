namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Enums;
using Backend.Domain.Entities;
using Backend.Shared.Resources;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using TokanPages.Services.UserService;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;

public class UploadUserMediaCommandHandlerTest : TestBase
{
    [Fact]
    public async Task WhenAddUserMediaCommand_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new UploadUserMediaCommand
        {
            UserId = userId,
            MediaTarget = UserMedia.UserImage,
            MediaName = DataUtilityService.GetRandomString(),
            MediaType = DataUtilityService.GetRandomString(),
            Data = new byte[1024]
        };

        var user = new Users
        {
            Id = userId,
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            IsActivated = true
        };

        var userInfo = new UserInfo
        {
            UserId = userId,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.SaveChangesAsync();
        
        var loggerService = new Mock<ILoggerService>();
        var storageFactory = new Mock<IAzureBlobStorageFactory>();
        var blobStorage = new Mock<IAzureBlobStorage>();
        var userService = new Mock<IUserService>();

        blobStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(string.Empty));

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        userService
            .Setup(service => service.GetActiveUser(
            It.IsAny<Guid?>(), 
            It.IsAny<bool>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UploadUserMediaCommandHandler(
            databaseContext,
            loggerService.Object, 
            storageFactory.Object, 
            userService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        blobStorage
            .Verify(storage => storage.UploadFile(
                It.IsAny<Stream>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()), 
            Times.Once());

        result.Should().NotBeNull();
        result.BlobName.Should().NotBeEmpty();
        result.BlobName.Should().Contain(command.MediaType);
        result.BlobName.Should().Contain(command.MediaName.ToUpper());
    }

    [Fact]
    public async Task GivenMissingUserInfo_WhenAddUserMediaCommand_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new UploadUserMediaCommand
        {
            UserId = userId,
            MediaTarget = UserMedia.UserImage,
            MediaName = DataUtilityService.GetRandomString(),
            MediaType = DataUtilityService.GetRandomString(),
            Data = new byte[1024]
        };

        var user = new Users
        {
            Id = userId,
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            IsActivated = true
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();
        
        var loggerService = new Mock<ILoggerService>();
        var storageFactory = new Mock<IAzureBlobStorageFactory>();
        var blobStorage = new Mock<IAzureBlobStorage>();
        var userService = new Mock<IUserService>();

        blobStorage
            .Setup(storage => storage.UploadFile(
                It.IsAny<Stream>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(string.Empty));

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        userService
            .Setup(service => service.GetActiveUser(
            It.IsAny<Guid?>(), 
            It.IsAny<bool>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UploadUserMediaCommandHandler(
            databaseContext,
            loggerService.Object, 
            storageFactory.Object, 
            userService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        result.Message.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }
}