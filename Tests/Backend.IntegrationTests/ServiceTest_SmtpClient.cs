using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.SmtpClient.Settings;

namespace Backend.IntegrationTests
{

    public class ServiceTest_SmtpClient
    {

        private readonly SmtpServerSettings FSmtpServer;

        public ServiceTest_SmtpClient() 
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<ServiceTest_SmtpClient>()
                .Build();

            FSmtpServer = Configuration.GetSection("SmtpServer").Get<SmtpServerSettings>();

        }

        [Fact]
        public async Task Should_SendMessage() 
        {

            // Arrange
            var LMailer = new SmtpClientService(FSmtpServer);
            LMailer.From = "contact@tomkandula.com";
            LMailer.Tos = new List<string>
            { 
                "tomasz.kandula@gmail.com", 
                "admin@tomkandula.com", 
                "tom@tomkandula.com" 
            };
            LMailer.Subject = "Integration Test / SmtpClient";
            LMailer.HtmlBody = $"<p>Test run Id: {Guid.NewGuid()}</p>";

            // Act
            var LResult = await LMailer.Send();

            // Assert
            LResult.Should().NotBeNull();
            LResult.ErrorDesc.Should().Be("n/a");
            LResult.IsSucceeded.Should().BeTrue();
        
        }

    }

}
