using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Shared.Models;

namespace Backend.IntegrationTests.Handlers.Mailer
{
    
    public class SendNewsletterCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public SendNewsletterCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task SendNewsletter_WhenEmailsAreProvided_ShouldReturnEmptyJsonObject()
        {

            // Arrange
            var LRequest = "/api/v1/mailer/sendnewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendNewsletterDto
            {
                SubscriberInfo = new List<SubscriberInfo>
                {
                    // Provide existing test emails to preview incoming messages
                    new SubscriberInfo
                    {
                        Id    = Guid.NewGuid().ToString(),
                        Email = DataProvider.GetRandomEmail()
                    },
                    new SubscriberInfo
                    {
                        Id    = Guid.NewGuid().ToString(),
                        Email = DataProvider.GetRandomEmail()
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

    }

}
