using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;
//TODO: redo tests for new implementation
public class UploadUserMediaCommandHandlerTest : TestBase
{
    // [Fact]
    // public async Task WhenAddUserMediaCommand_ShouldSucceed()
    // {
    //     // Arrange
    //     var userId = Guid.NewGuid();
    //     var command = new UploadUserMediaCommand
    //     {
    //         UserId = userId,
    //         MediaTarget = UserMedia.UserImage,
    //         MediaName = DataUtilityService.GetRandomString(),
    //         MediaType = DataUtilityService.GetRandomString(),
    //         Data = new byte[1024]
    //     };
    //
    //     var user = new Backend.Domain.Entities.Users
    //     {
    //         Id = userId,
    //         UserAlias = DataUtilityService.GetRandomString(5),
    //         EmailAddress = DataUtilityService.GetRandomEmail(),
    //         CryptedPassword = DataUtilityService.GetRandomString(),
    //         CreatedBy = userId,
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         IsActivated = true
    //     };
    //
    //     var userInfo = new UserInfo
    //     {
    //         UserId = userId,
    //         FirstName = DataUtilityService.GetRandomString(),
    //         LastName = DataUtilityService.GetRandomString(),
    //         CreatedBy = userId,
    //         CreatedAt = DataUtilityService.GetRandomDateTime()
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.UserInfo.AddAsync(userInfo);
    //     await databaseContext.SaveChangesAsync();
    //     
    //     var loggerService = new Mock<ILoggerService>();
    //     var storageFactory = new Mock<IAzureBlobStorageFactory>();
    //     var blobStorage = new Mock<IAzureBlobStorage>();
    //     var userService = new Mock<IUserService>();
    //
    //     blobStorage
    //         .Setup(storage => storage.UploadFile(
    //             It.IsAny<Stream>(),
    //             It.IsAny<string>(),
    //             It.IsAny<string>(),
    //             It.IsAny<CancellationToken>()))
    //         .Returns(Task.FromResult(string.Empty));
    //
    //     storageFactory
    //         .Setup(factory => factory.Create())
    //         .Returns(blobStorage.Object);
    //
    //     userService
    //         .Setup(service => service.GetActiveUser(
    //         It.IsAny<Guid?>(), 
    //         It.IsAny<bool>(), 
    //         It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(user);
    //
    //     var handler = new UploadUserMediaCommandHandler(
    //         databaseContext,
    //         loggerService.Object, 
    //         storageFactory.Object, 
    //         userService.Object);
    //
    //     // Act
    //     var result = await handler.Handle(command, CancellationToken.None);
    //
    //     // Assert
    //     blobStorage
    //         .Verify(storage => storage.UploadFile(
    //             It.IsAny<Stream>(), 
    //             It.IsAny<string>(), 
    //             It.IsAny<string>(), 
    //             It.IsAny<CancellationToken>()), 
    //         Times.Once());
    //
    //     result.Should().NotBeNull();
    //     result.BlobName.Should().NotBeEmpty();
    //     result.BlobName.Should().Contain(command.MediaType);
    //     result.BlobName.Should().Contain(command.MediaName.ToUpper());
    // }
    //
    // [Fact]
    // public async Task GivenMissingUserInfo_WhenAddUserMediaCommand_ShouldAddMissingUserInfo()
    // {
    //     // Arrange
    //     var userId = Guid.NewGuid();
    //     var command = new UploadUserMediaCommand
    //     {
    //         UserId = userId,
    //         MediaTarget = UserMedia.UserImage,
    //         MediaName = DataUtilityService.GetRandomString(),
    //         MediaType = DataUtilityService.GetRandomString(),
    //         Data = new byte[1024]
    //     };
    //
    //     var user = new Backend.Domain.Entities.Users
    //     {
    //         Id = userId,
    //         UserAlias = DataUtilityService.GetRandomString(5),
    //         EmailAddress = DataUtilityService.GetRandomEmail(),
    //         CryptedPassword = DataUtilityService.GetRandomString(),
    //         CreatedBy = userId,
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         IsActivated = true
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.SaveChangesAsync();
    //     
    //     var loggerService = new Mock<ILoggerService>();
    //     var storageFactory = new Mock<IAzureBlobStorageFactory>();
    //     var blobStorage = new Mock<IAzureBlobStorage>();
    //     var userService = new Mock<IUserService>();
    //
    //     blobStorage
    //         .Setup(storage => storage.UploadFile(
    //             It.IsAny<Stream>(),
    //             It.IsAny<string>(),
    //             It.IsAny<string>(),
    //             It.IsAny<CancellationToken>()))
    //         .Returns(Task.FromResult(string.Empty));
    //
    //     storageFactory
    //         .Setup(factory => factory.Create())
    //         .Returns(blobStorage.Object);
    //
    //     userService
    //         .Setup(service => service.GetActiveUser(
    //         It.IsAny<Guid?>(), 
    //         It.IsAny<bool>(), 
    //         It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(user);
    //
    //     var handler = new UploadUserMediaCommandHandler(
    //         databaseContext,
    //         loggerService.Object, 
    //         storageFactory.Object, 
    //         userService.Object);
    //
    //     // Act
    //     await handler.Handle(command, CancellationToken.None);
    //
    //     // Assert
    //     var hasUserInfo = databaseContext.UserInfo.Where(x => x.UserId == userId);
    //     hasUserInfo.Should().NotBeNull();
    // }
}