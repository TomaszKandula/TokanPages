namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Cqrs.Handlers.Commands.Mailer;
    using Shared.Services.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;
    using FluentAssertions;
    using MediatR;
    using Xunit;
    using Moq;

    public class SendNewsletterCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenSubscriberInfo_WhenSendNewsletter_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = DataUtilityService.GetRandomString(),
                Subject = DataUtilityService.GetRandomString(),
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new ()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorage>();
            var LMockedAppUrls = new Mock<ApplicationPaths>();

            var LSendActionResult = new ActionResult { IsSucceeded = true };
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.Send(CancellationToken.None))
                .Returns(Task.FromResult(LSendActionResult));

            var LMockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
            var LMockedResults = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = LMockedPayLoad
            };
            
            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(LMockedResults);

            var LSendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                LMockedLogger.Object, 
                LMockedCustomHttpClient.Object,
                LMockedSmtpClientService.Object, 
                LMockedTemplateHelper.Object, 
                LMockedAzureStorageSettings.Object, 
                LMockedAppUrls.Object);

            // Act
            var LResult = await LSendNewsletterCommandHandler.Handle(LSendNewsletterCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(await Task.FromResult(Unit.Value));
        }
    }
}