namespace TokanPages.WebApi.Tests.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Shared.Models;
    using Backend.Shared.Dto.Mailer;
    using Backend.Cqrs.Handlers.Commands.Mailer;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/Mailer";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        public MailerControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;     

        [Fact]
        public async Task GivenValidEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/SendMessage/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendMessageDto
            {
                FirstName = DataProviderService.GetRandomString(),
                LastName = DataProviderService.GetRandomString(),
                UserEmail = DataProviderService.GetRandomEmail(),
                EmailFrom = DataProviderService.GetRandomEmail(),
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
        public async Task GivenValidEmailsAndValidJwt_WhenSendNewsletter_ShouldReturnEmptyJsonObject()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/SendNewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendNewsletterDto
            {
                SubscriberInfo = new List<SubscriberInfoModel>
                {
                    new ()
                    {
                        Id    = Guid.NewGuid().ToString(),
                        Email = "tomasz.kandula@gmail.com"
                    }
                },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("{}");
        }

        [Fact]
        public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/SendNewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendNewsletterDto
            {
                SubscriberInfo = new List<SubscriberInfoModel>
                {
                    new ()
                    {
                        Id    = Guid.NewGuid().ToString(),
                        Email = "tomasz.kandula@gmail.com"
                    }
                },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetInvalidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        
        [Theory]
        [InlineData("john@gmail.com")]
        public async Task GivenValidEmailAndValidJwt_WhenVerifyEmailAddress_ShouldReturnResultsAsJsonObject(string AEmail)
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/VerifyEmailAddress/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new VerifyEmailAddressDto { Email = AEmail };

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

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