using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages;

namespace Backend.IntegrationTests.Handlers.Subscribers
{

    public class GetAllSubscribersQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetAllSubscribersQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnCollection()
        {
            // Arrange
            var LRequest = $"/api/v1/subscribers/getallsubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<IEnumerable<TokanPages.Backend.Domain.Entities.Subscribers>>(LContent);
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}
