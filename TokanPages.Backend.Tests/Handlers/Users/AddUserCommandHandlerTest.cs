namespace TokanPages.Backend.Tests.Handlers.Users
{   
    using Moq;
    using Moq.Protected;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.CipheringService;
    using Shared.Services.DateTimeService;
    using Shared.Services.TemplateService;

    public class AddUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenFieldsAreProvided_WhenAddUser_ShouldAddEntity() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Password = DataUtilityService.GetRandomString()
            };

            var LRoles = new Roles
            {
                Name = Identity.Authorization.Roles.EverydayUser.ToString(),
                Description = Identity.Authorization.Roles.EverydayUser.ToString()
            };

            var LPermissions = new List<Permissions>
            {
                new ()
                {
                    Name = Identity.Authorization.Permissions.CanSelectArticles.ToString()
                },
                new ()
                {
                    Name = Identity.Authorization.Permissions.CanSelectComments.ToString()
                }
            };

            var LDefaultPermissions = new List<DefaultPermissions>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Role = LRoles,
                    Permission = LPermissions[0]
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Role = LRoles,
                    Permission = LPermissions[1]
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Roles.AddAsync(LRoles);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.DefaultPermissions.AddRangeAsync(LDefaultPermissions);

            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedCipher = new Mock<ICipheringService>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedLogger = new Mock<ILogger>();
            var LMockedTemplateService = new Mock<ITemplateService>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();

            const string MOCKED_PASSWORD = "MockedPassword";
            LMockedCipher
                .Setup(ACipher => ACipher.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(MOCKED_PASSWORD);
            
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
            
            var LAddUserCommandHandler = new AddUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTime.Object, 
                LMockedCipher.Object,
                LMockedSmtpClientService.Object,
                LMockedLogger.Object,
                LMockedTemplateService.Object,
                LHttpClient,
                LMockedAzureStorage.Object,
                LMockedApplicationPaths.Object,
                LMockedExpirationSettings.Object
            );

            // Act
            await LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None);

            // Assert
            var LResult = LDatabaseContext.Users.ToList();

            LResult.Should().NotBeNull();
            LResult.Should().HaveCount(1);
            LResult[0].EmailAddress.Should().Be(LAddUserCommand.EmailAddress);
            LResult[0].UserAlias.Should().Be(LAddUserCommand.UserAlias.ToLower());
            LResult[0].FirstName.Should().Be(LAddUserCommand.FirstName);
            LResult[0].LastName.Should().Be(LAddUserCommand.LastName);
            LResult[0].IsActivated.Should().BeFalse();
            LResult[0].LastLogged.Should().BeNull();
            LResult[0].LastUpdated.Should().BeNull();
            LResult[0].AvatarName.Should().Be(Constants.Defaults.AVATAR_NAME);
            LResult[0].ShortBio.Should().BeNull();
            LResult[0].CryptedPassword.Should().HaveLength(MOCKED_PASSWORD.Length);
            LResult[0].ResetId.Should().BeNull();
            LResult[0].ResetIdEnds.Should().BeNull();
            LResult[0].ActivationId.Should().NotBeNull();
            LResult[0].ActivationIdEnds.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenNotActivatedUserWithExpiredActivation_WhenAddUser_ShouldAddEntity() 
        {
            // Arrange
            var LTestEmail = DataUtilityService.GetRandomEmail();
            var LAddUserCommand = new AddUserCommand 
            {
                EmailAddress = LTestEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Password = DataUtilityService.GetRandomString()
            };

            var LOldActivationIdEnds = DateTimeService.Now.AddMinutes(-30);
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = LTestEmail,
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTime.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = Guid.NewGuid(),
                ActivationIdEnds = LOldActivationIdEnds
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedCipher = new Mock<ICipheringService>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedLogger = new Mock<ILogger>();
            var LMockedTemplateService = new Mock<ITemplateService>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();

            var LExpirationSettings = new ExpirationSettings
            {
                ActivationIdExpiresIn = 30
            };

            const string MOCKED_PASSWORD = "MockedPassword";
            LMockedCipher
                .Setup(ACipher => ACipher.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(MOCKED_PASSWORD);

            LMockedDateTime
                .Setup(ADateTime => ADateTime.Now)
                .Returns(DateTimeService.Now);
            
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
            
            var LAddUserCommandHandler = new AddUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTime.Object, 
                LMockedCipher.Object,
                LMockedSmtpClientService.Object,
                LMockedLogger.Object,
                LMockedTemplateService.Object,
                LHttpClient,
                LMockedAzureStorage.Object,
                LMockedApplicationPaths.Object,
                LExpirationSettings
            );

            // Act
            await LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None);

            // Assert
            var LResult = LDatabaseContext.Users.ToList();

            LResult.Should().NotBeNull();
            LResult.Should().HaveCount(1);
            LResult[0].EmailAddress.Should().Be(LTestEmail);
            LResult[0].UserAlias.Should().Be(LUsers.UserAlias);
            LResult[0].FirstName.Should().Be(LUsers.FirstName);
            LResult[0].LastName.Should().Be(LUsers.LastName);
            LResult[0].IsActivated.Should().BeFalse();
            LResult[0].LastLogged.Should().BeNull();
            LResult[0].LastUpdated.Should().BeNull();
            LResult[0].AvatarName.Should().Be(LUsers.AvatarName);
            LResult[0].ShortBio.Should().BeNull();
            LResult[0].CryptedPassword.Should().HaveLength(MOCKED_PASSWORD.Length);
            LResult[0].ResetId.Should().BeNull();
            LResult[0].ResetIdEnds.Should().BeNull();
            LResult[0].ActivationId.Should().NotBeNull();
            LResult[0].ActivationIdEnds.Should().NotBeNull();
            LResult[0].ActivationIdEnds.Should().NotBe(LOldActivationIdEnds);
        }

        [Fact]
        public async Task GivenExistingEmail_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = DataUtilityService.GetRandomEmail();
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = LTestEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = LTestEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTime.Now,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedCipher = new Mock<ICipheringService>();
            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LMockedLogger = new Mock<ILogger>();
            var LMockedTemplateService = new Mock<ITemplateService>();
            var LMockedAzureStorage = new Mock<AzureStorage>();
            var LMockedApplicationPaths = new Mock<ApplicationPaths>();
            var LMockedExpirationSettings = new Mock<ExpirationSettings>();
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            
            LMockedCipher
                .Setup(ACipher => ACipher.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("MockedPassword");
            
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
            
            var LAddUserCommandHandler = new AddUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTime.Object, 
                LMockedCipher.Object,
                LMockedSmtpClientService.Object,
                LMockedLogger.Object,
                LMockedTemplateService.Object,
                LHttpClient,
                LMockedAzureStorage.Object,
                LMockedApplicationPaths.Object,
                LMockedExpirationSettings.Object
            );

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
        }
    }
}