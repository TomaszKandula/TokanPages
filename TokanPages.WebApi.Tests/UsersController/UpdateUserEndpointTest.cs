namespace TokanPages.WebApi.Tests.UsersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Dto.Users;
    using Newtonsoft.Json;
    using FluentAssertions;
    using Xunit;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenIncorrectIdNoJwt_WhenUpdateUser_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.Unauthorized);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().BeEmpty();
        }
    }
}