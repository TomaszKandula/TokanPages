namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Domain.Entities;
    using Cqrs.Handlers.Commands.Users;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class ResetUserPasswordCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidEmailAddress_WhenResetUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var user = new Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var resetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = user.EmailAddress
            };

            var mockedLogger = new Mock<ILogger>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateService = new Mock<ITemplateService>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedExpirationSettings = new Mock<ExpirationSettings>();
            var mockedAzureStorage = new Mock<AzureStorage>();
            var mockedApplicationPaths = new Mock<ApplicationPaths>();

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

            // Act
            var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object,
                mockedTemplateService.Object,
                mockedDateTimeService.Object,
                mockedAzureStorage.Object,
                mockedApplicationPaths.Object,
                mockedExpirationSettings.Object
            );
            await resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None);

            // Assert
            var userEntity = await databaseContext.Users.FindAsync(user.Id);

            userEntity.Should().NotBeNull();
            userEntity.EmailAddress.Should().Be(user.EmailAddress);
            userEntity.UserAlias.Should().Be(user.UserAlias);
            userEntity.FirstName.Should().Be(user.FirstName);
            userEntity.LastName.Should().Be(user.LastName);
            userEntity.IsActivated.Should().BeTrue();
            userEntity.LastUpdated.Should().NotBeNull();
            userEntity.LastLogged.Should().BeNull();
            userEntity.CryptedPassword.Should().BeEmpty();
            userEntity.ResetId.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenSmtpError_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var user = new Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var resetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = user.EmailAddress
            };

            var mockedLogger = new Mock<ILogger>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateService = new Mock<ITemplateService>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedExpirationSettings = new Mock<ExpirationSettings>();
            var mockedAzureStorage = new Mock<AzureStorage>();
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

            var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object,
                mockedTemplateService.Object,
                mockedDateTimeService.Object,
                mockedAzureStorage.Object,
                mockedApplicationPaths.Object,
                mockedExpirationSettings.Object
            );

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_SEND_EMAIL));
        }

        [Fact]
        public async Task GivenEmptyTemplate_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var user = new Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var resetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = user.EmailAddress
            };

            var mockedLogger = new Mock<ILogger>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedTemplateService = new Mock<ITemplateService>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedExpirationSettings = new Mock<ExpirationSettings>();
            var mockedAzureStorage = new Mock<AzureStorage>();
            var mockedApplicationPaths = new Mock<ApplicationPaths>();
            
            var sendActionResult = new ActionResult { IsSucceeded = false };
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

            var resetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedCustomHttpClient.Object,
                mockedSmtpClientService.Object,
                mockedTemplateService.Object,
                mockedDateTimeService.Object,
                mockedAzureStorage.Object,
                mockedApplicationPaths.Object,
                mockedExpirationSettings.Object
            );

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => resetUserPasswordCommandHandler.Handle(resetUserPasswordCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY));
        }
    }
}