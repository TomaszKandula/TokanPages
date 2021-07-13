namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Cqrs.Handlers.Commands.Mailer;
    using Shared.Services.TemplateService;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Moq.Protected;
    using MediatR;
    using Xunit;
    using Moq;

    public class SendNewsletterCommandHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public SendNewsletterCommandHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenSubscriberInfo_WhenSendNewsletter_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendNewsletterCommand = new SendNewsletterCommand
            {
                Message = FDataUtilityService.GetRandomString(),
                Subject = FDataUtilityService.GetRandomString(),
                SubscriberInfo = new List<SubscriberInfoModel>
                {
                    new ()
                    {
                        Email = FDataUtilityService.GetRandomEmail()
                    }
                }
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorageSettingsModel>();
            var LMockedAppUrls = new Mock<ApplicationPathsModel>();

            var LSendActionResult = new ActionResultModel { IsSucceeded = true };
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.Send(CancellationToken.None))
                .Returns(Task.FromResult(LSendActionResult));

            LMockedHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var LHttpClient = new HttpClient(LMockedHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };

            var LSendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                LMockedLogger.Object, 
                LHttpClient,
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