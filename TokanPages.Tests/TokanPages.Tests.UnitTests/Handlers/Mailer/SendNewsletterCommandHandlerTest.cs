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
using Backend.Dto.Mailer.Models;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Services.EmailSenderService;

public class SendNewsletterCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenSubscriberInfo_WhenSendNewsletter_ShouldFinishSuccessful()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = DataUtilityService.GetRandomString(),
            Subject = DataUtilityService.GetRandomString(),
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Email = DataUtilityService.GetRandomEmail()
                }
            }
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedApplicationSettings = MockApplicationSettings();

        var randomString = DataUtilityService.GetRandomString();
        mockedEmailSenderService
            .Setup(sender => sender.GetEmailTemplate(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(randomString);

        var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
            databaseContext,
            mockedLogger.Object, 
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object);

        // Act
        var result = await sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None);

        // Assert
        result.Should().Be(await Task.FromResult(Unit.Value));
    }

    [Fact]
    public async Task GivenEmptyEmailTemplate_WhenSendNewsletter_ShouldThrowError()
    {
        // Arrange
        var sendNewsletterCommand = new SendNewsletterCommand
        {
            Message = DataUtilityService.GetRandomString(),
            Subject = DataUtilityService.GetRandomString(),
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Email = DataUtilityService.GetRandomEmail()
                }
            }
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedEmailSenderService = new Mock<IEmailSenderService>();
        var mockedApplicationSettings = MockApplicationSettings();

        mockedEmailSenderService
            .Setup(sender => sender.GetEmailTemplate(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
            databaseContext,
            mockedLogger.Object, 
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL));
    }
}