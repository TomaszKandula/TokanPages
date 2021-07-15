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
    using Shared.Services.DateTimeService;
    using FluentAssertions;
    using Moq.Protected;
    using MediatR;
    using Xunit;
    using Moq;

    public class SendMessageCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
        {
            // Arrange
            var LSendMessageCommand = new SendMessageCommand
            {
                Subject = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Message = DataUtilityService.GetRandomString(),
                EmailFrom = DataUtilityService.GetRandomEmail(),
                EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
                UserEmail = DataUtilityService.GetRandomEmail()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorage>();
            var LDateTimeService = new Mock<IDateTimeService>();

            var LSendActionResult = new ActionResult { IsSucceeded = true };
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