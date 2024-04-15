using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Sender.Mailer.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Mailer;

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
        var mockedConfig = SetConfiguration();

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
            mockedConfig.Object, 
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
        var mockedConfig = SetConfiguration();

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
            mockedConfig.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL));
    }
}