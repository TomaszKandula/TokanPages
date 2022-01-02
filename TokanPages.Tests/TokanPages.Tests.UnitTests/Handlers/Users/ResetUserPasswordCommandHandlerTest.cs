namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using Backend.Domain.Entities;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Core.Utilities.DateTimeService;
using Backend.Core.Utilities.TemplateService;
using Backend.Core.Utilities.CustomHttpClient;
using Backend.Core.Utilities.CustomHttpClient.Models;

public class ResetUserPasswordCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidEmailAddress_WhenResetUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var user = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            EmailAddress = user.EmailAddress
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedTemplateService = new Mock<ITemplateService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();

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

        // Act
        var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedTemplateService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        await resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None);

        // Assert
        var userEntity = await databaseContext.Users.FindAsync(user.Id);

        userEntity.Should().NotBeNull();
        userEntity.EmailAddress.Should().Be(user.EmailAddress);
        userEntity.UserAlias.Should().Be(user.UserAlias);
        userEntity.FirstName.Should().Be(user.FirstName);
        userEntity.LastName.Should().Be(user.LastName);
        userEntity.IsActivated.Should().BeTrue();
        userEntity.LastUpdated.Should().NotBeNull();
        userEntity.LastLogged.Should().BeNull();
        userEntity.CryptedPassword.Should().BeEmpty();
        userEntity.ResetId.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenSmtpError_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            EmailAddress = user.EmailAddress
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedTemplateService = new Mock<ITemplateService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();

        var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.InternalServerError,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = mockedPayLoad
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);

        var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedTemplateService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
    }

    [Fact]
    public async Task GivenEmptyTemplate_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            EmailAddress = user.EmailAddress
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedTemplateService = new Mock<ITemplateService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedApplicationSettings = MockApplicationSettings();

        var mockedResults = new Results
        {
            StatusCode = HttpStatusCode.OK,
            ContentType = new MediaTypeHeaderValue("text/plain"),
            Content = null
        };

        mockedCustomHttpClient
            .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockedResults);

        var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedTemplateService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
    }
}