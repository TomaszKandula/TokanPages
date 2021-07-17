namespace TokanPages.WebApi.Tests.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Backend.Core.Extensions;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Articles;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using Backend.Database.Initializer.Data.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticlesControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenAllFieldsAreCorrectAsAnonymousUser_WhenAddArticle_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AddArticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AddArticleDto
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
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
            var LRequest = $"{API_BASE_URL}/AddArticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AddArticleDto
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), 
                FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
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
            var LRequest = $"{API_BASE_URL}/GetAllArticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

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
            var LRequest = $"{API_BASE_URL}/GetArticle/{LTestUserId}/";
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
            var LRequest = $"{API_BASE_URL}/GetArticle/{Guid.NewGuid()}/";

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
            var LRequest = $"{API_BASE_URL}/RemoveArticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

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
        public async Task GivenNoJwt_WhenUpdateArticleContent_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleContent/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleContentDto
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public async Task GivenInvalidArticleId_WhenUpdateArticleCount_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleCount/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleCountDto
            {
                Id = Guid.NewGuid()
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenInvalidArticleId_WhenUpdateArticleLikes_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleLikes/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleLikesDto
            {
                Id = Guid.NewGuid(),
                AddToLikes = 10
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenInvalidArticleIdAndValidJwt_WhenUpdateArticleVisibility_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleVisibility/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), 
                FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);

            var LPayLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ACCESS_DENIED);
        }
        
        [Fact]
        public async Task GivenInvalidArticleIdAndInvalidJwt_WhenUpdateArticleVisibility_ShouldReturnForbidden()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleVisibility/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetInvalidClaimsIdentity(), 
                FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);

            var LPayLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}