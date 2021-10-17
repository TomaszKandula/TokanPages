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
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsAuthenticated).Returns(true);
            
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object, 
                mockedLookupClient.Object, 
                smtpServerSettingsModel);
            
            // Act
            var results = await smtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            results.IsSucceeded.Should().BeTrue();
            results.ErrorCode.Should().BeNullOrEmpty();
            results.ErrorDesc.Should().BeNullOrEmpty();
            results.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpServer_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsConnected).Returns(false);
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsAuthenticated).Returns(false);
            
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object, 
                mockedLookupClient.Object, 
                smtpServerSettingsModel);
            
            // Act
            var results = await smtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            results.IsSucceeded.Should().BeFalse();
            results.ErrorCode.Should().Be(nameof(ErrorCodes.NOT_CONNECTED_TO_SMTP));
            results.ErrorDesc.Should().Be(ErrorCodes.NOT_CONNECTED_TO_SMTP);
            results.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpCredentials_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsAuthenticated).Returns(false);
            
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object, 
                mockedLookupClient.Object, 
                smtpServerSettingsModel);
            
            // Act
            var results = await smtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            results.IsSucceeded.Should().BeFalse();
            results.ErrorCode.Should().Be(nameof(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP));
            results.ErrorDesc.Should().Be(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP);
            results.InnerMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpHost_WhenConnectAndAuthenticate_ShouldThrowError()
        {
            // Arrange
            const string errorMessage = "The host is null";
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<SecureSocketOptions>(),
                        It.IsAny<CancellationToken>()))
                .Throws(new ArgumentException(errorMessage));

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsConnected).Returns(false);
            mockedSmtpClient.Setup(smtpClient => smtpClient.IsAuthenticated).Returns(false);
            
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object, 
                mockedLookupClient.Object, 
                smtpServerSettingsModel);
            
            // Act
            var results = await smtpClientService.CanConnectAndAuthenticate(CancellationToken.None);

            // Assert
            results.IsSucceeded.Should().BeFalse();
            results.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_CLIENT_ERROR));
            results.ErrorDesc.Should().Be(ErrorCodes.SMTP_CLIENT_ERROR);
            results.InnerMessage.Should().Be(errorMessage);
        }

        [Fact]
        public async Task GivenValidSettingsAndEmails_WhenSendEmail_ShouldReturnSuccess()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .SendAsync(
                        It.IsAny<MimeMessage>(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel)
            {
                Subject = DataUtilityService.GetRandomString(),
                From = DataUtilityService.GetRandomEmail(),
                Tos = new List<string> { DataUtilityService.GetRandomEmail() },
                Ccs = new List<string> { DataUtilityService.GetRandomEmail() },
                Bccs = new List<string> { DataUtilityService.GetRandomEmail() },
                PlainText = DataUtilityService.GetRandomString()
            };

            // Act
            var results = await smtpClientService.Send(CancellationToken.None);
            
            // Assert
            results.IsSucceeded.Should().BeTrue();
            results.ErrorCode.Should().BeNullOrEmpty();
            results.ErrorDesc.Should().BeNullOrEmpty();
            results.InnerMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GivenValidSettingsAndCorruptedSmtpServer_WhenSendEmail_ShouldThrowError()
        {
            // Arrange
            const string errorMessage = "Cannot send the email. Server responded with error";
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .SendAsync(
                        new MimeMessage(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        true, 
                        It.IsAny<CancellationToken>()))
                .Throws(new Exception(errorMessage));
            
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel)
            {
                Subject = DataUtilityService.GetRandomString(),
                From = DataUtilityService.GetRandomEmail(),
                Tos = new List<string> { DataUtilityService.GetRandomEmail() },
                Ccs = null,
                Bccs = null,
                PlainText = DataUtilityService.GetRandomString()
            };

            // Act
            var results = await smtpClientService.Send(CancellationToken.None);

            // Assert
            results.IsSucceeded.Should().BeFalse();
            results.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_CLIENT_ERROR));
            results.ErrorDesc.Should().Be(ErrorCodes.SMTP_CLIENT_ERROR);
            results.InnerMessage.Should().Be(errorMessage);
        }
        
        [Fact]
        public void GivenCorrectEmailAddress_WhenCheckingFormat_ShouldReturnSuccess()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            
            var emails = new List<string>
            {
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail()
            };

            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel); 
            
            // Act
            var result = smtpClientService.IsAddressCorrect(emails);

            // Assert
            result[0].Address.Should().Be(emails[0]);
            result[1].Address.Should().Be(emails[1]);
            result[2].Address.Should().Be(emails[2]);
            result[3].Address.Should().Be(emails[3]);

            result[0].IsValid.Should().BeTrue();
            result[1].IsValid.Should().BeTrue();
            result[2].IsValid.Should().BeTrue();
            result[3].IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenIncorrectEmailAddresses_WhenCheckingFormat_ShouldReturnInvalidEmailFlag()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            
            var emails = new List<string>
            {
                DataUtilityService.GetRandomString(),
                DataUtilityService.GetRandomString(),
            };

            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel); 
            
            // Act
            var result = smtpClientService.IsAddressCorrect(emails);

            // Assert
            result[0].Address.Should().Be(emails[0]);
            result[1].Address.Should().Be(emails[1]);

            result[0].IsValid.Should().BeFalse();
            result[1].IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task GivenDomainWithMxAndARecords_WhenCheckDomain_ShouldReturnTrue()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedDnsQueryResponse = new Mock<IDnsQueryResponse>();

            const string lookupDomain = "hotmail.com";
            IReadOnlyList<DnsResourceRecord> dnsResourceRecord = new[]
            {
                new ARecord(new ResourceRecordInfo(lookupDomain, ResourceRecordType.A, QueryClass.IN, 3600, 1024), IPAddress.None),
                new ARecord(new ResourceRecordInfo(lookupDomain, ResourceRecordType.AAAA, QueryClass.IN, 3600, 1024), IPAddress.None),
                new ARecord(new ResourceRecordInfo(lookupDomain, ResourceRecordType.MX, QueryClass.IN, 3600, 1024), IPAddress.None),
            };
            
            mockedDnsQueryResponse
                .SetupGet(dnsQueryResponse => dnsQueryResponse.Answers)
                .Returns(dnsResourceRecord);
    
            mockedLookupClient
                .Setup(lookupClient=> lookupClient
                    .QueryAsync(
                        It.IsAny<string>(), 
                        It.IsAny<QueryType>(), 
                        It.IsAny<QueryClass>(), 
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedDnsQueryResponse.Object);
    
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel); 
    
            // Act
            var result = await smtpClientService.IsDomainCorrect(DataUtilityService.GetRandomEmail(9, lookupDomain));
    
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GivenDomainWithOnlyCaaRecord_WhenCheckDomain_ShouldReturnFalse()
        {
            // Arrange
            var smtpServerSettingsModel = new SmtpServer();
            var mockedLookupClient = new Mock<ILookupClient>();
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedDnsQueryResponse = new Mock<IDnsQueryResponse>();

            const string lookupDomain = "hotmail.com";
            IReadOnlyList<DnsResourceRecord> dnsResourceRecord = new[]
            {
                new ARecord(new ResourceRecordInfo(lookupDomain, ResourceRecordType.CAA, QueryClass.IN, 3600, 1024), IPAddress.None),
            };
            
            mockedDnsQueryResponse
                .SetupGet(dnsQueryResponse => dnsQueryResponse.Answers)
                .Returns(dnsResourceRecord);
    
            mockedLookupClient
                .Setup(lookupClient=> lookupClient
                    .QueryAsync(
                        It.IsAny<string>(), 
                        It.IsAny<QueryType>(), 
                        It.IsAny<QueryClass>(), 
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedDnsQueryResponse.Object);
    
            var smtpClientService = new SmtpClientService(
                mockedSmtpClient.Object,
                mockedLookupClient.Object,
                smtpServerSettingsModel); 
    
            // Act
            var result = await smtpClientService.IsDomainCorrect(DataUtilityService.GetRandomEmail(9, lookupDomain));
    
            // Assert
            result.Should().BeFalse();
        }
    }
}