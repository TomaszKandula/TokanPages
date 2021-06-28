using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Database.Initializer.Data;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class ArticlesControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        private readonly DataProviderService FDataProviderService;

        public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
            FDataProviderService = new DataProviderService();
        }

        [Fact]
        public async Task GivenAllFieldsAreCorrectAsAnonymousUser_WhenAddArticle_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/addarticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddArticleDto
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenAllFieldsAreCorrectAsLoggedUser_WhenAddArticle_ShouldSucceed()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/addarticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddArticleDto
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode()
            };

            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(Claims.UserAlias, FDataProviderService.GetRandomString()),
                new Claim(Claims.Roles, Roles.EverydayUser),
                new Claim(Claims.UserId, User1.FId.ToString()),
                new Claim(Claims.FirstName, User1.FIRST_NAME),
                new Claim(Claims.LastName, User1.LAST_NAME),
                new Claim(Claims.EmailAddress, User1.EMAIL_ADDRESS)
            });

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, LGetValidClaims, FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/getallarticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(LContent)
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            var LTestUserId = Article1.FId;
            var LRequest = $"/api/v1/articles/getarticle/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LHttpClient = FWebAppFactory.CreateClient();
            var LRequest = $"/api/v1/articles/getarticle/{Guid.NewGuid()}/";

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/removearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new RemoveArticleDto
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
        public async Task GivenIncorrectIdAndNoJwt_WhenUpdateArticle_ShouldReturnUnauthorized()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/updatearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new UpdateArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(150),
                ImageToUpload = FDataProviderService.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
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