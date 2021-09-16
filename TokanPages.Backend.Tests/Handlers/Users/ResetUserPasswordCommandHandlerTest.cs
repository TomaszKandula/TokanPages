using TokanPages.Backend.Shared.Services.DateTimeService;

namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Shared.Services.TemplateService;
    using Cqrs.Handlers.Commands.Users;
    using Core.Exceptions;
    using Storage.Models;
    using Shared.Models;
    using Moq.Protected;
    using Core.Logger;
    using SmtpClient;

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
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();

            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();

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

            // Act
            var LResetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedLogger.Object,
                LHttpClient,
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
            LUserEntity.LastUpdated.Should().BeNull();
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
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();

            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();

            var LSendActionResult = new ActionResult { IsSucceeded = false };
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

            var LResetUserPasswordCommandHandler = new ResetUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedLogger.Object,
                LHttpClient,
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