using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
using TokanPages;

namespace Backend.IntegrationTests.Handlers.Users
{
    public class GetAllUsersQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetAllUsersQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory) 
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnCollection() 
        {
            // Arrange
            var LRequest = "/api/v1/users/getallusers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<IEnumerable<GetAllUsersQueryResult>>(LContent);
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}
