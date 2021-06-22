using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.SmtpClient.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using DnsClient;
using MimeKit;

namespace TokanPages.Backend.Tests.Services
{
    public class SmtpClientServiceTest
    {
        [Fact]
        public async Task GivenValidSmtpSettings_WhenConnectAndAuthenticate_ShouldReturnSuccess()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        SecureSocketOptions.None, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsConnected).Returns(true);
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsAuthenticated).Returns(true);
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object, 
                LMockedLookupClient.Object, 
                LSmtpServerSettingsModel);
            
            // Act
            var LResults = await LSmtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            LResults.IsSucceeded.Should().BeTrue();
            LResults.ErrorCode.Should().BeNullOrEmpty();
            LResults.ErrorDesc.Should().BeNullOrEmpty();
            LResults.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpServer_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        SecureSocketOptions.None, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsConnected).Returns(false);
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsAuthenticated).Returns(false);
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object, 
                LMockedLookupClient.Object, 
                LSmtpServerSettingsModel);
            
            // Act
            var LResults = await LSmtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            LResults.IsSucceeded.Should().BeFalse();
            LResults.ErrorCode.Should().Be(nameof(ErrorCodes.NOT_CONNECTED_TO_SMTP));
            LResults.ErrorDesc.Should().Be(ErrorCodes.NOT_CONNECTED_TO_SMTP);
            LResults.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpCredentials_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        SecureSocketOptions.None, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsConnected).Returns(true);
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsAuthenticated).Returns(false);
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object, 
                LMockedLookupClient.Object, 
                LSmtpServerSettingsModel);
            
            // Act
            var LResults = await LSmtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            LResults.IsSucceeded.Should().BeFalse();
            LResults.ErrorCode.Should().Be(nameof(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP));
            LResults.ErrorDesc.Should().Be(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP);
            LResults.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpHost_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            const string ERROR_MESSAGE = "The host is null";
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        SecureSocketOptions.None,
                        CancellationToken.None))
                .Throws(new ArgumentException(ERROR_MESSAGE));

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsConnected).Returns(false);
            LMockedSmtpClient.Setup(ASmtpClient => ASmtpClient.IsAuthenticated).Returns(false);
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object, 
                LMockedLookupClient.Object, 
                LSmtpServerSettingsModel);
            
            // Act
            var LResults = await LSmtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            LResults.IsSucceeded.Should().BeFalse();
            LResults.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_CLIENT_ERROR));
            LResults.ErrorDesc.Should().Be(ErrorCodes.SMTP_CLIENT_ERROR);
            LResults.InnerMessage.Should().Be(ERROR_MESSAGE);
        }

        [Fact]
        public async Task GivenValidSettingsAndEmails_WhenSendEmail_ShouldReturnSuccess()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        SecureSocketOptions.None, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .SendAsync(
                        new MimeMessage(),
                        CancellationToken.None, 
                        null))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel)
            {
                Subject = StringProvider.GetRandomString(),
                From = StringProvider.GetRandomEmail(),
                Tos = new List<string> {StringProvider.GetRandomEmail()},
                Ccs = null,
                Bccs = null,
                PlainText = StringProvider.GetRandomString()
            };

            // Act
            var LResults = await LSmtpClientService.Send(CancellationToken.None);
            
            // Assert
            LResults.IsSucceeded.Should().BeTrue();
            LResults.ErrorCode.Should().BeNullOrEmpty();
            LResults.ErrorDesc.Should().BeNullOrEmpty();
            LResults.InnerMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GivenValidSettingsAndCorruptedSmtpServer_WhenSendEmail_ShouldThrowError()
        {
            // Arrange
            const string ERROR_MESSAGE = "Cannot send the email. Server responded with error";
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        SecureSocketOptions.None, 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .SendAsync(
                        new MimeMessage(),
                        CancellationToken.None, 
                        null))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        CancellationToken.None))
                .Throws(new Exception(ERROR_MESSAGE));
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel)
            {
                Subject = StringProvider.GetRandomString(),
                From = StringProvider.GetRandomEmail(),
                Tos = new List<string> {StringProvider.GetRandomEmail()},
                Ccs = null,
                Bccs = null,
                PlainText = StringProvider.GetRandomString()
            };

            // Act
            var LResults = await LSmtpClientService.Send(CancellationToken.None);

            // Assert
            LResults.IsSucceeded.Should().BeFalse();
            LResults.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_CLIENT_ERROR));
            LResults.ErrorDesc.Should().Be(ErrorCodes.SMTP_CLIENT_ERROR);
            LResults.InnerMessage.Should().Be(ERROR_MESSAGE);
        }
        
        [Fact]
        public void GivenCorrectEmailAddress_WhenCheckingFormat_ShouldReturnSuccess()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            
            var LEmails = new List<string>
            {
                StringProvider.GetRandomEmail(),
                StringProvider.GetRandomEmail(),
                StringProvider.GetRandomEmail(),
                StringProvider.GetRandomEmail()
            };

            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel); 
            
            // Act
            var LResult = LSmtpClientService.IsAddressCorrect(LEmails);

            // Assert
            LResult[0].EmailAddress.Should().Be(LEmails[0]);
            LResult[1].EmailAddress.Should().Be(LEmails[1]);
            LResult[2].EmailAddress.Should().Be(LEmails[2]);
            LResult[3].EmailAddress.Should().Be(LEmails[3]);

            LResult[0].IsValid.Should().BeTrue();
            LResult[1].IsValid.Should().BeTrue();
            LResult[2].IsValid.Should().BeTrue();
            LResult[3].IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenIncorrectEmailAddresses_WhenCheckingFormat_ShouldReturnInvalidEmailFlag()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            
            var LEmails = new List<string>
            {
                StringProvider.GetRandomString(),
                StringProvider.GetRandomString(),
            };

            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel); 
            
            // Act
            var LResult = LSmtpClientService.IsAddressCorrect(LEmails);

            // Assert
            LResult[0].EmailAddress.Should().Be(LEmails[0]);
            LResult[1].EmailAddress.Should().Be(LEmails[1]);

            LResult[0].IsValid.Should().BeFalse();
            LResult[1].IsValid.Should().BeFalse();
        }
    }
}