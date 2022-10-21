using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.UserService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class ResetUserPasswordCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidEmailAddress_WhenResetUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var user = new Backend.Domain.Entities.Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
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

        var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedEmailSenderService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object, 
            mockedUserService.Object);

        // Act
        await resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None);

        // Assert
        var userEntity = await databaseContext.Users.FindAsync(user.Id);

        userEntity.Should().NotBeNull();
        userEntity.EmailAddress.Should().Be(user.EmailAddress);
        userEntity.UserAlias.Should().Be(user.UserAlias);
        userEntity.IsActivated.Should().BeTrue();
        userEntity.CryptedPassword.Should().BeEmpty();
        userEntity.ResetId.Should().NotBeNull();
    }
}