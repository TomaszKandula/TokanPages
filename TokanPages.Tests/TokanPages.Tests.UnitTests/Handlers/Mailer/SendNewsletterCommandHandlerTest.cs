using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Mailer.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.EmailSenderService;
using TokanPages.WebApi.Dto.Mailer.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Mailer;

public class SendNewsletterCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenSubscriberInfo_WhenSendNewsletter_ShouldFinishSuccessful()
    {
        // Arrange
        var command = new SendNewsletterCommand
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

        var handler = new SendNewsletterCommandHandler(
            databaseContext,
            mockedLogger.Object, 
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(await Task.FromResult(Unit.Value));
    }

    [Fact]
    public async Task GivenEmptyEmailTemplate_WhenSendNewsletter_ShouldThrowError()
    {
        // Arrange
        var command = new SendNewsletterCommand
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

        var handler = new SendNewsletterCommandHandler(
            databaseContext,
            mockedLogger.Object, 
            mockedEmailSenderService.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL));
    }
}