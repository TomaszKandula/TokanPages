namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Backend.Shared;
using Backend.Shared.Models;
using Backend.Core.Exceptions;
using Backend.Domain.Entities;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Services.CipheringService;
using Backend.Core.Utilities.DateTimeService;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Models;

public class AddUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenFieldsAreProvided_WhenAddUser_ShouldAddEntity() 
    {
        // Arrange
        var addUserCommand = new AddUserCommand 
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

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
                Role = roles,
                Permission = permissions[0]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = roles,
                Permission = permissions[1]
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Roles.AddAsync(roles);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.DefaultPermissions.AddRangeAsync(defaultPermissions);

        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();

        const string mockedPassword = "MockedPassword";
        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(mockedPassword);

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);

        var addUserCommandHandler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedCustomHttpClient.Object,
            mockedApplicationSettings.Object);

        // Act
        await addUserCommandHandler.Handle(addUserCommand, CancellationToken.None);

        // Assert
        var result = databaseContext.Users.ToList();

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].EmailAddress.Should().Be(addUserCommand.EmailAddress);
        result[0].UserAlias.Should().Be(addUserCommand.UserAlias.ToLower());
        result[0].FirstName.Should().Be(addUserCommand.FirstName);
        result[0].LastName.Should().Be(addUserCommand.LastName);
        result[0].IsActivated.Should().BeFalse();
        result[0].LastLogged.Should().BeNull();
        result[0].LastUpdated.Should().BeNull();
        result[0].AvatarName.Should().Be(Constants.Defaults.AvatarName);
        result[0].ShortBio.Should().BeNull();
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
        var addUserCommand = new AddUserCommand 
        {
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var oldActivationIdEnds = DateTimeService.Now.AddMinutes(-30);
        var users = new Users
        { 
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString().ToLower(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Registered = DateTime.Now,
            AvatarName = Constants.Defaults.AvatarName,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ActivationId = Guid.NewGuid(),
            ActivationIdEnds = oldActivationIdEnds
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();
            
        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();

        var expirationSettings = new ExpirationSettings { ActivationIdExpiresIn = 30 };
        var mockedApplicationSettings = MockApplicationSettings(expirationSettings: expirationSettings);

        const string mockedPassword = "MockedPassword";
        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedDateTime
            .Setup(dateTime => dateTime.Now)
            .Returns(DateTimeService.Now);

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };
            
        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);
            
        var addUserCommandHandler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedCustomHttpClient.Object,
            mockedApplicationSettings.Object);

        // Act
        await addUserCommandHandler.Handle(addUserCommand, CancellationToken.None);

        // Assert
        var result = databaseContext.Users.ToList();

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].EmailAddress.Should().Be(testEmail);
        result[0].UserAlias.Should().Be(users.UserAlias);
        result[0].FirstName.Should().Be(users.FirstName);
        result[0].LastName.Should().Be(users.LastName);
        result[0].IsActivated.Should().BeFalse();
        result[0].LastLogged.Should().BeNull();
        result[0].LastUpdated.Should().BeNull();
        result[0].AvatarName.Should().Be(users.AvatarName);
        result[0].ShortBio.Should().BeNull();
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
        var addUserCommand = new AddUserCommand
        {
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
        };

        var users = new Users
        { 
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Registered = DateTime.Now,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();
            
        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns("MockedPassword");

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };
            
        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);
            
        var addUserCommandHandler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedCustomHttpClient.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => addUserCommandHandler.Handle(addUserCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
    }

    [Fact]
    public async Task GivenEmptyEmailTemplate_WhenAddUser_ShouldThrowError() 
    {
        // Arrange
        var addUserCommand = new AddUserCommand 
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

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
                Role = roles,
                Permission = permissions[0]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = roles,
                Permission = permissions[1]
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Roles.AddAsync(roles);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.DefaultPermissions.AddRangeAsync(defaultPermissions);

        var mockedDateTime = new Mock<DateTimeService>();
        var mockedCipher = new Mock<ICipheringService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();

        const string mockedPassword = "MockedPassword";
        mockedCipher
            .Setup(service => service.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(mockedPassword);

        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = null
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);

        var addUserCommandHandler = new AddUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedCipher.Object,
            mockedCustomHttpClient.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => addUserCommandHandler.Handle(addUserCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
    }
}