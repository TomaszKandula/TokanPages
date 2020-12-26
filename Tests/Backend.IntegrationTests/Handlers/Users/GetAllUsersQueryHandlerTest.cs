using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages;

namespace Backend.IntegrationTests.Handlers.Users
{

    public class GetAllUsersQueryHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public GetAllUsersQueryHandlerTest(TestFixture<Startup> ACustomFixture) 
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnCollection() 
        {

            // Arrange
            var LRequest = $"/api/v1/users/getallusers/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<IEnumerable<TokanPages.Backend.Domain.Entities.Users>>(LContent);
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);

        }

    }

}
