namespace TokanPages.IntegrationTests.UsersController;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Shared.Dto.Users;

public partial class UsersControllerTest
{
    [Fact]
    public async Task GivenAllFieldsAreProvided_WhenAddUser_ShouldReturnNewGuid() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AddUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AddUserDto 
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory.CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }
}