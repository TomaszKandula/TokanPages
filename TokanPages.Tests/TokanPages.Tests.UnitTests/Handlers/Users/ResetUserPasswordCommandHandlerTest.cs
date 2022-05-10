namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Core.Utilities.DateTimeService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Models.Interfaces;

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
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        mockedEmailSenderService
            .Setup(sender => sender.SendNotification(It.IsAny<IConfiguration>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedEmailSenderService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object, 
            mockedUserService.Object);

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
}