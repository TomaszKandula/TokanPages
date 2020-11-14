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
        public async Task VerifyEmailAddress_Test(string Email) 
        {

            // Arrange
            var LRequest = $"/api/v1/mailer/inspection/{Email}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LResults = JsonConvert.DeserializeObject<EmailVerified>(LStringResponse);

            LResults.Error.ErrorDesc.Should().Be("n/a");
            LResults.IsFormatCorrect.Should().BeTrue();
            LResults.IsDomainCorrect.Should().BeTrue();

        }

        [Fact]
        public async Task SendMessage_Test() 
        {

            // Arrange
            var LRequest = "/api/v1/mailer/message/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new NewMessage
            {
                FirstName = "Tomasz",
                LastName  = "Kandula",
                UserEmail = "tomasz.kandula@gmail.com",
                EmailFrom = "contact@tomkandula.com",
                EmailTos  = new List<string> { "admin@tomkandula.com" },
                Subject   = "Test",
                Message   = $"Test run: {LNewGuid}.",
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LResponse = await FHttpClient.SendAsync(LNewRequest);
            var LContent = await LResponse.Content.ReadAsStringAsync();

            // Assert
            LResponse.StatusCode.Should().Be(200);
            var LDeserialized = JsonConvert.DeserializeObject<MessagePosted>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Fact]
        public async Task SendNewsletter_Test() 
        {

            // Arrange
            var LRequest = "/api/v1/mailer/newsletter/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new NewMessage
            {
                EmailFrom = "contact@tomkandula.com",
                EmailTos = new List<string> { "admin@tomkandula.com", "tomasz.kandula@gmail.com" },
                Subject = "Newsletter Test",
                Message = $"Integration test run. ID: {LNewGuid}. Put newsletter content here.",
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LResponse = await FHttpClient.SendAsync(LNewRequest);
            var LContent = await LResponse.Content.ReadAsStringAsync();

            // Assert
            LResponse.StatusCode.Should().Be(200);
            var LDeserialized = JsonConvert.DeserializeObject<MessagePosted>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

    }

}
