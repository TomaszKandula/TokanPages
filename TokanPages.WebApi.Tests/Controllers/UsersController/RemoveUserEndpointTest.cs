namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Dto.Users;
    using Newtonsoft.Json;
    using Xunit;

    public class RemoveUserEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/users";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public RemoveUserEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenRemoveUser_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/RemoveUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new RemoveUserDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.Unauthorized);
        }
    }
}