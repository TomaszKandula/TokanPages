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
    using Shared.Services.DateTimeService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;
    using FluentAssertions;
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
            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateHelper = new Mock<ITemplateService>();
            var LMockedAzureStorageSettings = new Mock<AzureStorage>();
            var LDateTimeService = new Mock<IDateTimeService>();

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
            
            var LSendMessageCommandHandler = new SendMessageCommandHandler(
                LMockedLogger.Object,
                LMockedCustomHttpClient.Object,
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