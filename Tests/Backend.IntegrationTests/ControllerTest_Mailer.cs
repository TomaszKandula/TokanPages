using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using BackEnd.IntegrationTests.Configuration;
using TokanPages;
using TokanPages.BackEnd.Controllers.Mailer.Model;
using TokanPages.BackEnd.Controllers.Mailer.Model.Responses;

namespace BackEnd.IntegrationTests
{

    public class ControllerTest_Mailer : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public ControllerTest_Mailer(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Theory]
        [InlineData("john@gmail.com")]
        public async Task Should_VerifyEmailAddress(string Email) 
        {

            // Arrange
            var LRequest = $"/api/v1/mailer/inspection/{Email}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<EmailVerified>(LContent);
            LDeserialized.Error.ErrorDesc.Should().Be("n/a");
            LDeserialized.IsFormatCorrect.Should().BeTrue();
            LDeserialized.IsDomainCorrect.Should().BeTrue();

        }

        [Fact]
        public async Task Should_SendUserMessage() 
        {

            // Arrange
            var LRequest = "/api/v1/mailer/message/";

            var LPayLoad = new SendMessage
            {
                FirstName = "Tomasz",
                LastName  = "Kandula",
                UserEmail = "tomasz.kandula@gmail.com",
                EmailFrom = "", // can be empty
                EmailTos  = new List<string> { "" }, // can be empty
                Subject   = "Integration Test / HttpClient / Endpoint",
                Message   = $"Test run Id: {Guid.NewGuid()}.",
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<MessagePosted>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Fact]
        public async Task Should_SendNewsletter() 
        {

            // Arrange
            var LRequest = "/api/v1/mailer/newsletter/";

            var LPayLoad = new SendNewsletter
            {
                SubscribersData = new List<SubscriberData> 
                { 
                    new SubscriberData
                    { 
                        Id    = "352e356e-1865-412e-bade-a2016dfde55f",
                        Email = "admin@tomkandula.com"
                    },
                    new SubscriberData
                    {
                        Id    = "7306a5d1-48cb-4dc4-9968-3dd8631b3b0b",
                        Email = "tom@tomkandula.com"
                    }
                },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<MessagePosted>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

    }

}
