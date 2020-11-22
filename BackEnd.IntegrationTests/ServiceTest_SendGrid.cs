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
        public async Task Should_SendEmail()
        {

            // Arrange
            var LSendGridService = new SendGridService(FSendGridKeys);

            LSendGridService.From      = "mailer@tomkandula.com";
            LSendGridService.Tos       = new List<string> { "tomasz.kandula@gmail.com" };
            LSendGridService.Subject   = "Integration Test / SendGrid service";
            LSendGridService.PlainText = string.Empty;
            LSendGridService.HtmlBody  = $"<p>Run test Id: {Guid.NewGuid()}</p>";

            // Act
            var LResult = await LSendGridService.Send();

            // Assert
            LResult.StatusCode.Should().Be(HttpStatusCode.Accepted);

        }

    }

}
