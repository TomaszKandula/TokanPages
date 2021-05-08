using Xunit;
using Moq;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.DataProviders;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient.Models;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using MediatR;

namespace TokanPages.UnitTests.Handlers.Mailer
{
    public class SendMessageCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                Subject = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                Message = StringProvider.GetRandomString(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string>{ StringProvider.GetRandomEmail() },
                UserEmail = StringProvider.GetRandomEmail()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateHelper>();
            var LMockedFileUtilityService = new Mock<IFileUtilityService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorageSettings>();
            var LDateTimeService = new Mock<IDateTimeService>();

            var LSendActionResult = new SendActionResult { IsSucceeded = true };
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.Send())
                .Returns(Task.FromResult(LSendActionResult));

            var LSendMessageCommandHandler = new SendMessageCommandHandler(
                LMockedSmtpClientService.Object, 
                LMockedTemplateHelper.Object, 
                LMockedFileUtilityService.Object, 
                LDateTimeService.Object,
                LMockedAzureStorageSettings.Object,
                LMockedLogger.Object);

            // Act
            var LResult = await LSendMessageCommandHandler.Handle(LSendMessageCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(await Task.FromResult(Unit.Value));
        }
    }
}