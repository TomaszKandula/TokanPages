using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Database.Seeders;

namespace Backend.IntegrationTests.Handlers.Users
{

    public class GetUserQueryHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public GetUserQueryHandlerTest(TestFixture<Startup> ACustomFixture) 
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task GetUser_WhenIdIsCorrect_ShouldReturnEntityAsJsonObject() 
        {

            // Arrange
            var LTestUserId = UsersSeeder.Dummy1.Id;
            var LRequest = $"/api/v1/users/getuser/{LTestUserId}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<TokanPages.Backend.Domain.Entities.Users>(LContent);
            LDeserialized.Should().NotBeNull();

        }

        [Fact]
        public async Task GetUser_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {

            // Arrange
            var LRequest = $"/api/v1/users/getuser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);

        }

    }

}
