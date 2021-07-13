namespace TokanPages.Backend.Tests.Services
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SmtpClient;
    using Shared.Resources;
    using SmtpClient.Models;
    using DnsClient.Protocol;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using FluentAssertions;
    using DnsClient;
    using MimeKit;
    using Xunit;
    using Moq;

    public class SmtpClientServiceTest : TestBase
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
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
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
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
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
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
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
                        It.IsAny<SecureSocketOptions>(),
                        It.IsAny<CancellationToken>()))
                .Throws(new ArgumentException(ERROR_MESSAGE));

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
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
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .SendAsync(
                        It.IsAny<MimeMessage>(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel)
            {
                Subject = DataUtilityService.GetRandomString(),
                From = DataUtilityService.GetRandomEmail(),
                Tos = new List<string> { DataUtilityService.GetRandomEmail() },
                Ccs = new List<string> { DataUtilityService.GetRandomEmail() },
                Bccs = new List<string> { DataUtilityService.GetRandomEmail() },
                PlainText = DataUtilityService.GetRandomString()
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
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .SendAsync(
                        new MimeMessage(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            LMockedSmtpClient
                .Setup(ASmtpClient => ASmtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Throws(new Exception(ERROR_MESSAGE));
            
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel)
            {
                Subject = DataUtilityService.GetRandomString(),
                From = DataUtilityService.GetRandomEmail(),
                Tos = new List<string> { DataUtilityService.GetRandomEmail() },
                Ccs = null,
                Bccs = null,
                PlainText = DataUtilityService.GetRandomString()
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
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail()
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
                DataUtilityService.GetRandomString(),
                DataUtilityService.GetRandomString(),
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

        [Fact]
        public async Task GivenDomainWithMxAndARecords_WhenCheckDomain_ShouldReturnTrue()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            var LMockedDnsQueryResponse = new Mock<IDnsQueryResponse>();

            const string LOOKUP_DOMAIN = "hotmail.com";
            IReadOnlyList<DnsResourceRecord> LDnsResourceRecord = new[]
            {
                new ARecord(new ResourceRecordInfo(LOOKUP_DOMAIN, ResourceRecordType.A, QueryClass.IN, 3600, 1024), IPAddress.None),
                new ARecord(new ResourceRecordInfo(LOOKUP_DOMAIN, ResourceRecordType.AAAA, QueryClass.IN, 3600, 1024), IPAddress.None),
                new ARecord(new ResourceRecordInfo(LOOKUP_DOMAIN, ResourceRecordType.MX, QueryClass.IN, 3600, 1024), IPAddress.None),
            };
            
            LMockedDnsQueryResponse
                .SetupGet(ADnsQueryResponse => ADnsQueryResponse.Answers)
                .Returns(LDnsResourceRecord);
    
            LMockedLookupClient
                .Setup(ALookupClient=> ALookupClient
                    .QueryAsync(
                        It.IsAny<string>(), 
                        It.IsAny<QueryType>(), 
                        It.IsAny<QueryClass>(), 
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(LMockedDnsQueryResponse.Object);
    
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel); 
    
            // Act
            var LResult = await LSmtpClientService.IsDomainCorrect(DataUtilityService.GetRandomEmail(9, LOOKUP_DOMAIN));
    
            // Assert
            LResult.Should().BeTrue();
        }

        [Fact]
        public async Task GivenDomainWithOnlyCaaRecord_WhenCheckDomain_ShouldReturnFalse()
        {
            // Arrange
            var LSmtpServerSettingsModel = new SmtpServerSettingsModel();
            var LMockedLookupClient = new Mock<ILookupClient>();
            var LMockedSmtpClient = new Mock<ISmtpClient>();
            var LMockedDnsQueryResponse = new Mock<IDnsQueryResponse>();

            const string LOOKUP_DOMAIN = "hotmail.com";
            IReadOnlyList<DnsResourceRecord> LDnsResourceRecord = new[]
            {
                new ARecord(new ResourceRecordInfo(LOOKUP_DOMAIN, ResourceRecordType.CAA, QueryClass.IN, 3600, 1024), IPAddress.None),
            };
            
            LMockedDnsQueryResponse
                .SetupGet(ADnsQueryResponse => ADnsQueryResponse.Answers)
                .Returns(LDnsResourceRecord);
    
            LMockedLookupClient
                .Setup(ALookupClient=> ALookupClient
                    .QueryAsync(
                        It.IsAny<string>(), 
                        It.IsAny<QueryType>(), 
                        It.IsAny<QueryClass>(), 
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(LMockedDnsQueryResponse.Object);
    
            var LSmtpClientService = new SmtpClientService(
                LMockedSmtpClient.Object,
                LMockedLookupClient.Object,
                LSmtpServerSettingsModel); 
    
            // Act
            var LResult = await LSmtpClientService.IsDomainCorrect(DataUtilityService.GetRandomEmail(9, LOOKUP_DOMAIN));
    
            // Assert
            LResult.Should().BeFalse();
        }
    }
}