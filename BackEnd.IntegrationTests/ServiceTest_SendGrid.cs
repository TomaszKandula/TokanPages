using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.SendGrid;
using TokanPages.BackEnd.Settings;

namespace BackEnd.IntegrationTests
{

    public class ServiceTest_SendGrid
    {

        private readonly SendGridKeys FSendGridKeys;

        public ServiceTest_SendGrid()
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<ServiceTest_SendGrid>()
                .Build();

            FSendGridKeys = Configuration.GetSection("SendGridKeys").Get<SendGridKeys>();

        }

        [Fact]
        public async Task Send_Test()
        {

            // Arrange
            var LSendGridService = new SendGridService(FSendGridKeys);

            LSendGridService.From      = "contact@tomkandula.com";
            LSendGridService.Tos       = new List<string> { "tom@tomkandula.com" };
            LSendGridService.Subject   = "Integration Test";
            LSendGridService.PlainText = string.Empty;
            LSendGridService.HtmlBody  = $"<p><b>Run Test</b></p><p>ID: {Guid.NewGuid()}</p>";

            // Act
            var LResult = await LSendGridService.Send();

            // Assert
            LResult.StatusCode.Should().Be(HttpStatusCode.Accepted);

        }

    }

}
