using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.DataProviders;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace TokanPages.IntegrationTests.Controllers
{
    public class MailerControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        public MailerControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;
     
        [Fact]
        public async Task GivenProvidedEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
        {
            // Arrange
            const string REQUEST = "/api/v1/mailer/sendmessage/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new SendMessageDto
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                UserEmail = StringProvider.GetRandomEmail(),
                EmailFrom = StringProvider.GetRandomEmail(),
                EmailTos = new List<string> { string.Empty },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"Test run Id: {Guid.NewGuid()}.",
            };

            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("{}");
        }

        [Fact]
        public async Task GivenProvidedEmails_WhenSendNewsletter_ShouldReturnEmptyJsonObject()
        {
            // Arrange
            const string REQUEST = "/api/v1/mailer/sendnewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new SendNewsletterDto
            {
                SubscriberInfo = new List<SubscriberInfo>
                {
                    new SubscriberInfo
                    {
                        Id    = Guid.NewGuid().ToString(),
                        Email = "tomasz.kandula@gmail.com"
                    }
                },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("{}");
        }
        
        [Theory]
        [InlineData("john@gmail.com")]
        public async Task GivenCorrectEmail_WhenVerifyEmailAddress_ShouldReturnResultsAsJsonObject(string AEmail)
        {
            // Arrange
            const string REQUEST = "/api/v1/mailer/verifyemailaddress/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new VerifyEmailAddressDto { Email = AEmail };
            var LHttpClient = FWebAppFactory.CreateClient();

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<VerifyEmailAddressCommandResult>(LContent);
            LDeserialized.IsFormatCorrect.Should().BeTrue();
            LDeserialized.IsDomainCorrect.Should().BeTrue();
        }
    }
}