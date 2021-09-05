namespace TokanPages.WebApi.Tests.Controllers.SubscribersController
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Subscribers;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class AddSubscriberEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/subscribers";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public AddSubscriberEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AddSubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new AddSubscriberDto { Email = DataUtilityService.GetRandomEmail() };

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
    }
}