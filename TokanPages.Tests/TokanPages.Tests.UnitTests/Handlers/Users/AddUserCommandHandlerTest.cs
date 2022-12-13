using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.ApplicationSettings.Models;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.CipheringService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.UserService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class AddUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenFieldsAreProvided_WhenAddUser_ShouldAddEntity() 
    {
        // Arrange
        var command = new AddUserCommand 
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var expectedUserAlias = $"{command.FirstName[..2]}{command.LastName[..3]}".ToLower();
        
        var roles = new Roles
        {
            Name = Backend.Domain.Enums.Roles.EverydayUser.ToString(),
            Description = Backend.Domain.Enums.Roles.EverydayUser.ToString()
        };

        var permissions = new List<Permissions>
        {
            new()
            {
                Name = Backend.Domain.Enums.Permissions.CanSelectArticles.ToString()
            },
            new()
            {
                Name = Backend.Domain.Enums.Permissions.CanSelectComments.ToString()
            }
        };

        var defaultPermissions = new List<DefaultPermissions>
        {
            new()
            {
                Id = Guid.NewGuid(),
                RoleNavigation = roles,
                PermissionNavigation = permissions[0]
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleNavigation = roles,
                PermissionNavigation = permissions[1]
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Roles.AddAsync(roles);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.DefaultPermissions.AddRangeAsync(defaultPermissions);

        const string mockedPassword = "MockedPassword";
        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedBlobStorage = new Mock<IAzureBlobStorage>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        mockedBlobStorage
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StorageStreamContent)null!);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedBlobStorage.Object);

        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedEmailSenderService
            .Setup(sender => sender.SendNotification(It.IsAny<IConfiguration>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object, 
            mockedAzureStorage.Object, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var result = databaseContext.Users.ToList();

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].EmailAddress.Should().Be(command.EmailAddress);
        result[0].UserAlias.Should().Be(expectedUserAlias);
        result[0].IsActivated.Should().BeFalse();
        result[0].CryptedPassword.Should().HaveLength(mockedPassword.Length);
        result[0].ResetId.Should().BeNull();
        result[0].ResetIdEnds.Should().BeNull();
        result[0].ActivationId.Should().NotBeNull();
        result[0].ActivationIdEnds.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenNotActivatedUserWithExpiredActivation_WhenAddUser_ShouldAddEntity() 
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var command = new AddUserCommand 
        {
            EmailAddress = testEmail,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var userAlias = $"{command.FirstName[..2]}{command.LastName[..3]}".ToLower();
        var oldActivationIdEnds = DateTimeService.Now.AddMinutes(-30);
        var users = new Backend.Domain.Entities.Users
        { 
            EmailAddress = testEmail,
            UserAlias = userAlias,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ActivationId = Guid.NewGuid(),
            ActivationIdEnds = oldActivationIdEnds
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();
            
        const string mockedPassword = "MockedPassword";
        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedUserService = new Mock<IUserService>();
        var expirationSettings = new LimitSettings { ActivationIdExpiresIn = 30 };
        var mockedApplicationSettings = MockApplicationSettings(limitSettings: expirationSettings);

        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedDateTime
            .Setup(dateTime => dateTime.Now)
            .Returns(DateTimeService.Now);

        mockedEmailSenderService
            .Setup(sender => sender.SendNotification(It.IsAny<IConfiguration>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        var handler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object, 
            mockedAzureStorage.Object, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var result = databaseContext.Users.ToList();

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].EmailAddress.Should().Be(testEmail);
        result[0].UserAlias.Should().Be(users.UserAlias);
        result[0].IsActivated.Should().BeFalse();
        result[0].CryptedPassword.Should().HaveLength(mockedPassword.Length);
        result[0].ResetId.Should().BeNull();
        result[0].ResetIdEnds.Should().BeNull();
        result[0].ActivationId.Should().NotBeNull();
        result[0].ActivationIdEnds.Should().NotBeNull();
        result[0].ActivationIdEnds.Should().NotBe(oldActivationIdEnds);
    }

    [Fact]
    public async Task GivenExistingEmail_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var command = new AddUserCommand
        {
            EmailAddress = testEmail,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
        };

        var users = new Backend.Domain.Entities.Users
        { 
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns("MockedPassword");

        mockedEmailSenderService
            .Setup(sender => sender.SendNotification(It.IsAny<IConfiguration>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object, 
            mockedAzureStorage.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
    }
}