namespace TokanPages.UnitTests.Handlers.Mailer
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
    using Backend.Shared.Models;
    using Backend.Core.Exceptions;
    using Backend.Shared.Resources;
    using Backend.Core.Utilities.LoggerService;
    using Backend.Cqrs.Handlers.Commands.Mailer;
    using Backend.Core.Utilities.TemplateService;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

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
                    new()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedApplicationSettings = MockApplicationSettings();

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
                databaseContext,
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedTemplateHelper.Object, 
                mockedApplicationSettings.Object);

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
                    new()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedApplicationSettings = MockApplicationSettings();

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
                databaseContext,
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedTemplateHelper.Object, 
                mockedApplicationSettings.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None));
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
                    new()
                    {
                        Email = DataUtilityService.GetRandomEmail()
                    }
                }
            };

            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedApplicationSettings = MockApplicationSettings();

            var mockedPayLoad = DataUtilityService.GetRandomStream().ToArray();
            var mockedResults = new Results
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = mockedPayLoad
            };
            
            mockedCustomHttpClient
                .Setup(client => client.Execute(It.IsAny<Configuration>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedResults);

            var sendNewsletterCommandHandler = new SendNewsletterCommandHandler(
                databaseContext,
                mockedLogger.Object, 
                mockedCustomHttpClient.Object,
                mockedTemplateHelper.Object, 
                mockedApplicationSettings.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => sendNewsletterCommandHandler.Handle(sendNewsletterCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
        }
    }
}