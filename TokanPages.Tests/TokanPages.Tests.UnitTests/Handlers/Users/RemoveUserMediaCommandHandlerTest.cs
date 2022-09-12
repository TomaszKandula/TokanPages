namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using MediatR;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Utilities.LoggerService;
using Backend.Application.Handlers.Commands.Users;
using TokanPages.Services.UserService;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;

public class RemoveUserMediaCommandHandlerTest : TestBase
{
    [Fact]
    public async Task WhenRemoveUserMediaCommand_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var blobName = DataUtilityService.GetRandomString();
        var command = new RemoveUserMediaCommand { UniqueBlobName = blobName };

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
            UserImageName = blobName,
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
            .Setup(storage => storage.DeleteFile(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        userService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new RemoveUserMediaCommandHandler(
            databaseContext,
            loggerService.Object, 
            userService.Object,
            storageFactory.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        blobStorage
            .Verify(storage => storage.DeleteFile(
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()), 
            Times.Once());

        result.Should().Be(Unit.Value);
    }
}