namespace TokanPages.WebApi.Tests.UsersController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Cqrs.Handlers.Queries.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenValidJwt_WhenGetAllUsers_ShouldReturnCollection() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/GetAllUsers/";
            var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            var deserialized = (JsonConvert.DeserializeObject<IEnumerable<GetAllUsersQueryResult>>(content) ?? Array.Empty<GetAllUsersQueryResult>())
                .ToList();
            deserialized.Should().NotBeNullOrEmpty();
            deserialized.Should().HaveCountGreaterThan(0);
        }
    }
}