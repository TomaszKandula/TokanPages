using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http.Headers;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Shared.Dto.Subscribers;
using TokanPages.Backend.Database.Initializer.Data.Users;
using TokanPages.Backend.Shared.Services.DataProviderService;
using TokanPages.Backend.Database.Initializer.Data.Subscribers;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class SubscribersControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        private readonly DataProviderService FDataProviderService;
        
        public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
            FDataProviderService = new DataProviderService();
        }
        
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/addsubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new AddSubscriberDto { Email = FDataProviderService.GetRandomEmail() };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.IsGuid().Should().BeTrue();
        }
        
        [Fact]
        public async Task GivenNoJwt_WhenGetAllSubscribers_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getallsubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public async Task GivenCorrectIdAndNoJwt_WhenGetSubscriber_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LTestUserId = Subscriber1.FId;
            var LRequest = $"/api/v1/subscribers/getsubscriber/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getsubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, REQUEST);
            
            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(Claims.UserAlias, FDataProviderService.GetRandomString()),
                new Claim(Claims.Roles, Roles.EverydayUser),
                new Claim(Claims.UserId, User1.FId.ToString()),
                new Claim(Claims.FirstName, User1.FIRST_NAME),
                new Claim(Claims.LastName, User1.LAST_NAME),
                new Claim(Claims.EmailAddress, User1.EMAIL_ADDRESS)
            });
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, LGetValidClaims, FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/removesubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new RemoveSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenUpdateSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/updatesubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new UpdateSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Email = FDataProviderService.GetRandomEmail(),
                Count = null,
                IsActivated = null
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}