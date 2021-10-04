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
            var LUser = new TokanPages.Backend.Domain.Entities.Users
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LResetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = LUser.EmailAddress
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateService = new Mock<ITemplateService>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();

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

            // Act
            var LResetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedLogger.Object,
                LMockedCustomHttpClient.Object,
                LMockedSmtpClientService.Object,
                LMockedTemplateService.Object,
                LMockedDateTimeService.Object,
                LMockedAzureStorage.Object,
                LMockedApplicationPaths.Object,
                LMockedExpirationSettings.Object
            );
            await LResetUserPasswordCommandHandler.Handle(LResetUserPasswordCommand, CancellationToken.None);

            // Assert
            var LUserEntity = await LDatabaseContext.Users.FindAsync(LUser.Id);

            LUserEntity.Should().NotBeNull();
            LUserEntity.EmailAddress.Should().Be(LUser.EmailAddress);
            LUserEntity.UserAlias.Should().Be(LUser.UserAlias);
            LUserEntity.FirstName.Should().Be(LUser.FirstName);
            LUserEntity.LastName.Should().Be(LUser.LastName);
            LUserEntity.IsActivated.Should().BeTrue();
            LUserEntity.LastUpdated.Should().NotBeNull();
            LUserEntity.LastLogged.Should().BeNull();
            LUserEntity.CryptedPassword.Should().BeEmpty();
            LUserEntity.ResetId.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenSmtpError_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUser = new TokanPages.Backend.Domain.Entities.Users
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LResetUserPasswordCommand = new ResetUserPasswordCommand
            {
                EmailAddress = LUser.EmailAddress
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedTemplateService = new Mock<ITemplateService>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();
            
            var LSendActionResult = new ActionResult { IsSucceeded = false };
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

            var LResetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedLogger.Object,
                LMockedCustomHttpClient.Object,
                LMockedSmtpClientService.Object,
                LMockedTemplateService.Object,
                LMockedDateTimeService.Object,
                LMockedAzureStorage.Object,
                LMockedApplicationPaths.Object,
                LMockedExpirationSettings.Object
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LResetUserPasswordCommandHandler.Handle(LResetUserPasswordCommand, CancellationToken.None));
        }
    }
}