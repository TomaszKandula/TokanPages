using Xunit;
using Moq;
using Moq.Protected;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Core.Services.DataProviderService;
using MediatR;

namespace TokanPages.Backend.Tests.Handlers.Mailer
{
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