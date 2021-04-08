using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Database.Dummies;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace Backend.IntegrationTests.Handlers.Subscribers
{
    public class GetSubscriberQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetSubscriberQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetSubscriber_WhenIdIsCorrect_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = Subscribers1.FId;
            var LRequest = $"/api/v1/subscribers/getsubscriber/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetSubscriberQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSubscriber_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getsubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
    }
}
