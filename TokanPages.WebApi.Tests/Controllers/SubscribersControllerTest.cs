namespace TokanPages.WebApi.Tests.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Core.Extensions;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Subscribers;
    using Backend.Database.Initializer.Data.Subscribers;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class SubscribersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/subscribers";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AddSubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new AddSubscriberDto { Email = DataProviderService.GetRandomEmail() };

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
            var LRequest = $"{API_BASE_URL}/GetAllSubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public async Task GivenCorrectIdAndNoJwt_WhenGetSubscriber_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LTestUserId = Subscriber1.FId;
            var LRequest = $"{API_BASE_URL}/GetSubscriber/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetSubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
            var LRequest = $"{API_BASE_URL}/RemoveSubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new RemoveSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenUpdateSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateSubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Email = DataProviderService.GetRandomEmail(),
                Count = null,
                IsActivated = null
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}