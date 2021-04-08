using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Database.Dummies;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace Backend.IntegrationTests.Handlers.Users
{
    public class GetUserQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetUserQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory) 
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetUser_WhenIdIsCorrect_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = User1.FId;
            var LRequest = $"/api/v1/users/getuser/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUser_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/users/getuser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
        }
    }
}
