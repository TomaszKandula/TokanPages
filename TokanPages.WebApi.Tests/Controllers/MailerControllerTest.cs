using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Database.Initializer.Data.Users;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class MailerControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        private readonly DataProviderService FDataProviderService;
        
        public MailerControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
            FDataProviderService = new DataProviderService();
        }
     
        [Fact]
        public async Task GivenValidEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
        {
            // Arrange
            const string REQUEST = "/api/v1/mailer/sendmessage/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new SendMessageDto
            {
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                UserEmail = FDataProviderService.GetRandomEmail(),
                EmailFrom = FDataProviderService.GetRandomEmail(),
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
            const string REQUEST = "/api/v1/mailer/sendnewsletter/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

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
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, GetValidClaims(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
        
        [Theory]
        [InlineData("john@gmail.com")]
        public async Task GivenValidEmailAndValidJwt_WhenVerifyEmailAddress_ShouldReturnResultsAsJsonObject(string AEmail)
        {
            // Arrange
            const string REQUEST = "/api/v1/mailer/verifyemailaddress/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new VerifyEmailAddressDto { Email = AEmail };

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, GetValidClaims(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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

        private ClaimsIdentity GetValidClaims()
        {
            return new (new[]
            {
                new Claim(Claims.UserAlias, FDataProviderService.GetRandomString()),
                new Claim(Claims.Roles, Roles.EverydayUser),
                new Claim(Claims.UserId, User1.FId.ToString()),
                new Claim(Claims.FirstName, User1.FIRST_NAME),
                new Claim(Claims.LastName, User1.LAST_NAME),
                new Claim(Claims.EmailAddress, User1.EMAIL_ADDRESS)
            });
        }
    }
}