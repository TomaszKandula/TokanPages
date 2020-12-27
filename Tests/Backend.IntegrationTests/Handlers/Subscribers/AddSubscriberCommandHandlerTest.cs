using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Subscribers;

namespace Backend.IntegrationTests.Handlers.Subscribers
{

    public class AddSubscriberCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public AddSubscriberCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task AddSubscriber_WhenAllFieldsAreCorrect_ShouldReturnNewGuid() 
        {

            // Arrange
            var LRequest = $"/api/v1/subscribers/addsubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AddSubscriberDto
            {
                Email = "tokan1@gmail.com"
            };

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

        }

    }

}
