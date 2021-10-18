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
    using Database;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Mailer;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class SendMessageCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenFilledUserForm_WhenSendMessage_ShouldFinishSuccessful()
        {
            // Arrange
            var sendMessageCommand = new SendMessageCommand
            {
                Subject = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Message = DataUtilityService.GetRandomString(),
                EmailFrom = DataUtilityService.GetRandomEmail(),
                EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
                UserEmail = DataUtilityService.GetRandomEmail()
            };

            var mockedDatabase = new Mock<DatabaseContext>();
            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedDateTimeService = new Mock<IDateTimeService>();

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
            
            var sendMessageCommandHandler = new SendMessageCommandHandler(
                mockedDatabase.Object,
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedDateTimeService.Object,
                mockedAzureStorageSettings.Object);

            // Act
            var result = await sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None);

            // Assert
            result.Should().Be(await Task.FromResult(Unit.Value));
        }

        [Fact]
        public async Task GivenEmptyEmailTemplate_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var sendMessageCommand = new SendMessageCommand
            {
                Subject = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Message = DataUtilityService.GetRandomString(),
                EmailFrom = DataUtilityService.GetRandomEmail(),
                EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
                UserEmail = DataUtilityService.GetRandomEmail()
            };

            var mockedDatabase = new Mock<DatabaseContext>();
            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedDateTimeService = new Mock<IDateTimeService>();

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
            
            var sendMessageCommandHandler = new SendMessageCommandHandler(
                mockedDatabase.Object,
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedDateTimeService.Object,
                mockedAzureStorageSettings.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
        }

        [Fact]
        public async Task GivenRemoteSmtpFailure_WhenSendMessage_ShouldThrowError()
        {
            // Arrange
            var sendMessageCommand = new SendMessageCommand
            {
                Subject = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Message = DataUtilityService.GetRandomString(),
                EmailFrom = DataUtilityService.GetRandomEmail(),
                EmailTos = new List<string>{ DataUtilityService.GetRandomEmail() },
                UserEmail = DataUtilityService.GetRandomEmail()
            };

            var mockedDatabase = new Mock<DatabaseContext>();
            var mockedLogger = new Mock<ILogger>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateHelper = new Mock<ITemplateService>();
            var mockedAzureStorageSettings = new Mock<AzureStorage>();
            var mockedDateTimeService = new Mock<IDateTimeService>();

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
            
            var sendMessageCommandHandler = new SendMessageCommandHandler(
                mockedDatabase.Object,
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object, 
                mockedTemplateHelper.Object, 
                mockedDateTimeService.Object,
                mockedAzureStorageSettings.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => sendMessageCommandHandler.Handle(sendMessageCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
        }
    }
}