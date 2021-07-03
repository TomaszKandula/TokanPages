using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
using TokanPages.Backend.Database.Initializer.Data.Users;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        private readonly DataProviderService FDataProviderService;

        public UsersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
            FDataProviderService = new DataProviderService();
        }
        
        [Fact]
        public async Task GivenAllFieldsAreProvided_WhenAddUser_ShouldReturnNewGuid() 
        {
            // Arrange
            const string REQUEST = "/api/v1/users/adduser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddUserDto 
            { 
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                Password = FDataProviderService.GetRandomString()
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
            const string REQUEST = "/api/v1/users/getallusers/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, REQUEST);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, GetValidClaims(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
            var LRequest = $"/api/v1/users/getuser/{LTestUserId}/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, GetValidClaims(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
        public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/users/getuser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, REQUEST);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, GetValidClaims(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
            const string REQUEST = "/api/v1/users/removeuser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

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
            const string REQUEST = "/api/v1/users/updateuser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new UpdateUserDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        private ClaimsIdentity GetValidClaims()
        {
            return new (new[]
            {
                new Claim(Claims.UserAlias, FDataProviderService.GetRandomString()),
                new Claim(Claims.Roles, Roles.EverydayUser),
                new Claim(Claims.UserId, User1.FId.ToString()),
                new Claim(Claims.FirstName, User1.FIRST_NAME),
                new Claim(Claims.LastName, User1.LAST_NAME),
                new Claim(Claims.EmailAddress, User1.EMAIL_ADDRESS)
            });
        }
    }
}