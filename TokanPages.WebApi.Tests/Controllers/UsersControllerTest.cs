namespace TokanPages.WebApi.Tests.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Users;
    using Backend.Database.Initializer.Data.Users;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class UsersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/users";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public UsersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenAllFieldsAreProvided_WhenAddUser_ShouldReturnNewGuid() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AddUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AddUserDto 
            { 
                EmailAddress = DataProviderService.GetRandomEmail(),
                UserAlias = DataProviderService.GetRandomString(),
                FirstName = DataProviderService.GetRandomString(),
                LastName = DataProviderService.GetRandomString(),
                Password = DataProviderService.GetRandomString()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenValidJwt_WhenGetAllUsers_ShouldReturnCollection() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetAllUsers/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = DataProviderService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<IEnumerable<GetAllUsersQueryResult>>(LContent).ToList();
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = User1.FId;
            var LRequest = $"{API_BASE_URL}/GetUser/{LTestUserId}/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = DataProviderService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenCorrectIdAndInvalidJwt_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = User1.FId;
            var LRequest = $"{API_BASE_URL}/GetUser/{LTestUserId}/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = DataProviderService.GenerateJwt(LTokenExpires, GetInvalidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        
        [Fact]
        public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetUser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = DataProviderService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
        }
        
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
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenIncorrectIdNoJwt_WhenUpdateUser_ShouldReturnUnauthorized() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                EmailAddress = DataProviderService.GetRandomEmail(),
                UserAlias = DataProviderService.GetRandomString(),
                FirstName = DataProviderService.GetRandomString(),
                LastName = DataProviderService.GetRandomString(),
                IsActivated = true
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}