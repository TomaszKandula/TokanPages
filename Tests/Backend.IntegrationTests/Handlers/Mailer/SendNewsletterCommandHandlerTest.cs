using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Shared.Models;

namespace Backend.IntegrationTests.Handlers.Mailer
{
    
    public class SendNewsletterCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public SendNewsletterCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
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
                    new SubscriberInfo
                    {
                        Id    = "352e356e-1865-412e-bade-a2016dfde55f",
                        Email = "admin@tomkandula.com"
                    },
                    new SubscriberInfo
                    {
                        Id    = "7306a5d1-48cb-4dc4-9968-3dd8631b3b0b",
                        Email = "tom@tomkandula.com"
                    }
                },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
            };

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("{}");

        }

    }

}
