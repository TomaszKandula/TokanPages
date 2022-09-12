namespace TokanPages.Tests.UnitTests.Handlers.Mailer;

using Moq;
using Xunit;
using MediatR;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Application.Handlers.Commands.Mailer;
using Backend.Core.Utilities.DateTimeService;
using TokanPages.Services.EmailSenderService;

public class SendMessageCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
    {
        // Arrange
        var command = new SendMessageCommand
        {
            Subject = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
            UserEmail = DataUtilityService.GetRandomEmail()
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        var randomString = DataUtilityService.GetRandomString();
        mockedEmailSenderService
            .Setup(sender => sender.GetEmailTemplate(
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(randomString);

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        var handler = new SendMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedEmailSenderService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object, 
            mockedUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(await Task.FromResult(Unit.Value));
    }

    [Fact]
    public async Task GivenEmptyEmailTemplate_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var command = new SendMessageCommand
        {
            Subject = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
            UserEmail = DataUtilityService.GetRandomEmail()
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedUserService
            .Setup(service => service.GetRequestUserTimezoneOffset())
            .Returns(-120);

        mockedEmailSenderService
            .Setup(sender => sender.GetEmailTemplate(
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var handler = new SendMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedEmailSenderService.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL));
    }
}