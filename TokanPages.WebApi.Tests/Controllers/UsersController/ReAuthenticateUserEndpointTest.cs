namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class ReAuthenticateUserEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/users";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public ReAuthenticateUserEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenNoRefreshTokensSaved_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LCookieValue = DataUtilityService.GetRandomString();
            var LRequest = $"{API_BASE_URL}/ReAuthenticateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ReAuthenticateUserDto
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            LNewRequest.Headers.Add("Cookie", $"{Constants.CookieNames.REFRESH_TOKEN}={LCookieValue};");
            
            // Assert
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.INVALID_REFRESH_TOKEN);            
        }
    }
}