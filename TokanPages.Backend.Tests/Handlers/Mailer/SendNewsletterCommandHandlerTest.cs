namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    using Moq;
    using Xunit;
    using MediatR;
    using FluentAssertions;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Mailer;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

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
                    new ()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedAppUrls = new Mock<ApplicationPaths>();

            var sendActionResult = new ActionResult { IsSucceeded = true };
            mockedSmtpClientService
                .Setup(client => client.Send(CancellationToken.None))
                .Returns(Task.FromResult(sendActionResult));

            var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
            var mockedResults = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = mockedPayLoad
            };
            
            mockedCustomHttpClient
                .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedResults);

            var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedAzureStorageSettings.Object, 
                mockedAppUrls.Object);

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
                    new ()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedApplicationPaths = new Mock<ApplicationPaths>();

            var sendActionResult = new ActionResult { IsSucceeded = true };
            mockedSmtpClientService
                .Setup(client => client.Send(CancellationToken.None))
                .Returns(Task.FromResult(sendActionResult));

            var mockedResults = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = null
            };
            
            mockedCustomHttpClient
                .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedResults);

            var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedAzureStorageSettings.Object, 
                mockedApplicationPaths.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
        }

        [Fact]
        public async Task GivenRemoteSmtpFailure_WhenSendNewsletter_ShouldThrowError()
        {
            // Arrange
            var sendNewsletterCommand = new SendNewsletterCommand
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

            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedApplicationPaths = new Mock<ApplicationPaths>();

            var sendActionResult = new ActionResult { IsSucceeded = false };
            mockedSmtpClientService
                .Setup(client => client.Send(CancellationToken.None))
                .Returns(Task.FromResult(sendActionResult));

            var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
            var mockedResults = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = mockedPayLoad
            };
            
            mockedCustomHttpClient
                .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedResults);

            var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedAzureStorageSettings.Object, 
                mockedApplicationPaths.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
        }
    }
}