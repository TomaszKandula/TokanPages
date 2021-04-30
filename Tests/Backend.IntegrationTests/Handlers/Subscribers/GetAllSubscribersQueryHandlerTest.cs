using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;
using TokanPages;

namespace Backend.IntegrationTests.Handlers.Subscribers
{

    public class GetAllSubscribersQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetAllSubscribersQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GetAllUsers_ShouldReturnCollection()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getallsubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllSubscribersQueryResult>>(LContent)
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}
