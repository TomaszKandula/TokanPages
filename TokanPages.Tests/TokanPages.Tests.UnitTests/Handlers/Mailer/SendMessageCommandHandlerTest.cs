namespace TokanPages.Tests.UnitTests.Handlers.Mailer;

using Moq;
using Xunit;
using MediatR;
using FluentAssertions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Mailer;
using Backend.Core.Utilities.DateTimeService;
using Backend.Core.Utilities.CustomHttpClient;
using Backend.Core.Utilities.CustomHttpClient.Models;

public class SendMessageCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
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
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
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
            
        var sendMessageCommandHandler = new SendMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        // Act
        var result = await sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None);

        // Assert
        result.Should().Be(await Task.FromResult(Unit.Value));
    }

    [Fact]
    public async Task GivenEmptyEmailTemplate_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
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
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
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
            
        var sendMessageCommandHandler = new SendMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
    }

    [Fact]
    public async Task GivenRemoteSmtpFailure_WhenSendMessage_ShouldThrowError()
    {
        // Arrange
        var sendMessageCommand = new SendMessageCommand
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
        var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
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
            
        var sendMessageCommandHandler = new SendMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedCustomHttpClient.Object,
            mockedDateTimeService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
    }
}