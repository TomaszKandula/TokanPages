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
    using Shared.Services.TemplateHelper;
    using Shared.Services.DateTimeService;
    using Shared.Services.DataProviderService;
    using FluentAssertions;
    using Moq.Protected;
    using MediatR;
    using Xunit;
    using Moq;

    public class SendMessageCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public SendMessageCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                Subject = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                Message = FDataProviderService.GetRandomString(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
                EmailTos = new List<string>{ FDataProviderService.GetRandomEmail() },
                UserEmail = FDataProviderService.GetRandomEmail()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateHelper>();
            var LMockedAzureStorageSettings = new Mock<AzureStorageSettingsModel>();
            var LDateTimeService = new Mock<IDateTimeService>();

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
            
            var LSendMessageCommandHandler = new SendMessageCommandHandler(
                LMockedLogger.Object,
                LHttpClient,
                LMockedSmtpClientService.Object, 
                LMockedTemplateHelper.Object, 
                LDateTimeService.Object,
                LMockedAzureStorageSettings.Object);

            // Act
            var LResult = await LSendMessageCommandHandler.Handle(LSendMessageCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(await Task.FromResult(Unit.Value));
        }
    }
}