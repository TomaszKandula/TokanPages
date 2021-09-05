namespace TokanPages.WebApi.Tests.Controllers.MailerController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Shared.Models;
    using Backend.Shared.Dto.Mailer;
    using Newtonsoft.Json;
    using Xunit;

    public class SendNewsletterEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/Mailer";

        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public SendNewsletterEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/SendNewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendNewsletterDto
            {
                SubscriberInfo = new List<SubscriberInfo>
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
            await EnsureStatusCode(LResponse, HttpStatusCode.Forbidden);
        }
    }
}