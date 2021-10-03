namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Cqrs.Handlers.Queries.Users;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenValidJwt_WhenGetAllUsers_ShouldReturnCollection() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetAllUsers/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = (JsonConvert.DeserializeObject<IEnumerable<GetAllUsersQueryResult>>(LContent) ?? Array.Empty<GetAllUsersQueryResult>())
                .ToList();
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}