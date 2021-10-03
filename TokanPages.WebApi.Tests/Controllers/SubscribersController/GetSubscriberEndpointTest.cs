namespace TokanPages.WebApi.Tests.Controllers.SubscribersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Resources;
    using Backend.Database.Initializer.Data.Subscribers;
    using FluentAssertions;
    using Xunit;

    public partial class SubscribersControllerTest
    {
        [Fact]
        public async Task GivenCorrectIdAndNoJwt_WhenGetSubscriber_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LTestUserId = Subscriber1.FId;
            var LRequest = $"{API_BASE_URL}/GetSubscriber/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetSubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
    }
}