namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using Xunit;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Users;
    using Backend.Database.Initializer.Data.Users;

    public class ResetUserPasswordEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/users";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public ResetUserPasswordEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenUserEmail_WhenResetUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ResetUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ResetUserPasswordDto
            {
                EmailAddress = User3.EMAIL_ADDRESS
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenInvalidUserEmail_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ResetUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ResetUserPasswordDto
            {
                EmailAddress = DataUtilityService.GetRandomEmail()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
        }
    }
}