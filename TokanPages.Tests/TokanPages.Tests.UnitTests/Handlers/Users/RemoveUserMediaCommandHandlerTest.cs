using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class RemoveUserMediaCommandHandlerTest : TestBase
{
    [Fact]
    public async Task WhenRemoveUserMediaCommand_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var blobName = DataUtilityService.GetRandomString();
        var command = new RemoveUserMediaCommand { UniqueBlobName = blobName };

        var user = new Backend.Domain.Entities.User.Users
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

        var mockedLogger = new Mock<ILoggerService>();
        var mockedStorageFactory = new Mock<IAzureBlobStorageFactory>();
        var mockedBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedUserService = new Mock<IUserService>();

        mockedBlobStorage
            .Setup(storage => storage.DeleteFile(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockedStorageFactory
            .Setup(factory => factory.Create(mockedLogger.Object))
            .Returns(mockedBlobStorage.Object);

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new RemoveUserMediaCommandHandler(
            databaseContext,
            mockedLogger.Object, 
            mockedUserService.Object,
            mockedStorageFactory.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedBlobStorage
            .Verify(storage => storage.DeleteFile(
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()), 
            Times.Once());

        result.Should().Be(Unit.Value);
    }
}