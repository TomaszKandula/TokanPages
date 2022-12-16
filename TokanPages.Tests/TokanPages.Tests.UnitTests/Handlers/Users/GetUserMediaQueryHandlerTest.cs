using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetUserMediaQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetUserMedia_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserMediaQuery
        {
            Id = userId,
            BlobName = DataUtilityService.GetRandomString()
        };

        var user = new Backend.Domain.Entities.Users
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

        userService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        const int contentBytes = 1024;
        const string contentType = "image/jpg";
        var streamContent = new StorageStreamContent
        {
            Content = new MemoryStream(new byte[contentBytes]),
            ContentType = contentType
        };

        blobStorage
            .Setup(storage => storage.OpenRead(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(streamContent);

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        var handler = new GetUserMediaQueryHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.FileContents.Should().NotBeNull();
        result.FileContents.Length.Should().Be(contentBytes);
        result.ContentType.Should().Be(contentType);
    }

    [Fact]
    public async Task GivenNoUser_WhenGetUserMedia_ShouldThrowError()
    {
        // Arrange
        var blobName = DataUtilityService.GetRandomString();
        var query = new GetUserMediaQuery
        {
            Id = Guid.NewGuid(),
            BlobName = blobName
        };

        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            CreatedBy = Guid.NewGuid(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            IsActivated = true
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var loggerService = new Mock<ILoggerService>();
        var storageFactory = new Mock<IAzureBlobStorageFactory>();

        var handler = new GetUserMediaQueryHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenNoStreamContent_WhenGetUserMedia_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserMediaQuery
        {
            Id = userId,
            BlobName = DataUtilityService.GetRandomString()
        };

        var user = new Backend.Domain.Entities.Users
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

        userService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        blobStorage
            .Setup(storage => storage.OpenRead(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((StorageStreamContent)null!);

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        var handler = new GetUserMediaQueryHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    [Fact]
    public async Task GivenNoStorageContent_WhenGetUserMedia_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserMediaQuery
        {
            Id = userId,
            BlobName = DataUtilityService.GetRandomString()
        };

        var user = new Backend.Domain.Entities.Users
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

        userService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var streamContent = new StorageStreamContent
        {
            Content = null,
            ContentType = "image/jpg"
        };

        blobStorage
            .Setup(storage => storage.OpenRead(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(streamContent);

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        var handler = new GetUserMediaQueryHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    [Fact]
    public async Task GivenNoStorageContentType_WhenGetUserMedia_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserMediaQuery
        {
            Id = userId,
            BlobName = DataUtilityService.GetRandomString()
        };

        var user = new Backend.Domain.Entities.Users
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

        userService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var streamContent = new StorageStreamContent
        {
            Content = new MemoryStream(new byte[1024]),
            ContentType = null
        };

        blobStorage
            .Setup(storage => storage.OpenRead(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(streamContent);

        storageFactory
            .Setup(factory => factory.Create())
            .Returns(blobStorage.Object);

        var handler = new GetUserMediaQueryHandler(
            databaseContext, 
            loggerService.Object, 
            storageFactory.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }
}