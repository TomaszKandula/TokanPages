namespace TokanPages.WebApi.Tests.MailerController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Shared.Models;
    using Backend.Shared.Dto.Mailer;

    public partial class MailerControllerTest
    {
        [Fact]
        public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/SendNewsletter/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new SendNewsletterDto
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
            
            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Forbidden);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEmpty();
        }
    }
}