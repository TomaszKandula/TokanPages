using Xunit;
using Moq;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient.Models;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using MediatR;

namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    public class SendNewsletterCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenSubscriberInfo_WhenSendNewsletter_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = StringProvider.GetRandomString(),
                Subject = StringProvider.GetRandomString(),
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new ()
                    {
                        Email = StringProvider.GetRandomEmail()
                    }
                }
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateHelper>();
            var LMockedFileUtilityService = new Mock<IFileUtilityService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorageSettings>();
            var LMockedAppUrls = new Mock<AppUrls>();

            var LSendActionResult = new SendActionResult { IsSucceeded = true };
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.Send(CancellationToken.None))
                .Returns(Task.FromResult(LSendActionResult));
            
            var LSendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                LMockedLogger.Object, 
                LMockedSmtpClientService.Object, 
                LMockedTemplateHelper.Object, 
                LMockedFileUtilityService.Object, 
                LMockedAzureStorageSettings.Object, 
                LMockedAppUrls.Object);

            // Act
            var LResult = await LSendNewsletterCommandHandler.Handle(LSendNewsletterCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(await Task.FromResult(Unit.Value));
        }
    }
}