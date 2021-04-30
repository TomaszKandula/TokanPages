using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Subscribers;

namespace Backend.IntegrationTests.Handlers.Subscribers
{
    public class AddSubscriberCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public AddSubscriberCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task AddSubscriber_WhenAllFieldsAreCorrect_ShouldReturnNewGuid() 
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/addsubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new AddSubscriberDto { Email = DataProvider.GetRandomEmail() };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            GuidTest.Check(LContent).Should().BeTrue();
        }
    }
}
