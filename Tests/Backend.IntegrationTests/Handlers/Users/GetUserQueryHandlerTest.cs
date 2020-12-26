using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

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
        public async Task GetUser_WhenIdIsCorrect_ShouldReturnJsonObject() 
        {

            // Arrange
            var LRequest = $"/api/v1/users/getuser/99780F77-3522-4E65-9FFD-9467EA448404/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<TokanPages.Backend.Domain.Entities.Users>(LContent);
            LDeserialized.Should().NotBeNull();

        }

    }

}
