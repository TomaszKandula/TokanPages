namespace TokanPages.Tests.IntegrationTests.Controllers;

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
using Microsoft.AspNetCore.TestHost;
using Backend.Core.Extensions;
using Backend.Shared.Resources;
using Backend.Shared.Dto.Articles;
using Backend.Cqrs.Handlers.Queries.Articles;
using Backend.Database.Initializer.Data.Articles;
using Factories;

public class ArticlesControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/articles";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/GetAllArticles/?IsPublished=false";

        // Act
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = (JsonConvert
                .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(content) ?? Array.Empty<GetAllArticlesQueryResult>())
            .ToList();
            
        deserialized.Should().NotBeNullOrEmpty();
        deserialized.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
    {
        // Arrange
        var testUserId = Article1.Id;
        var request = $"{ApiBaseUrl}/GetArticle/{testUserId}/?noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(content);
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();
        var request = $"{ApiBaseUrl}/GetArticle/{Guid.NewGuid()}/?noCache=true";

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrectAsAnonymousUser_WhenAddArticle_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AddArticle/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AddArticleDto
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrectAsLoggedUser_WhenAddArticle_ShouldSucceed()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AddArticle/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AddArticleDto
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();
        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/RemoveArticle/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new RemoveArticleDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenNoJwt_WhenUpdateArticleContent_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateArticleContent/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateArticleContentDto
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
            System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenInvalidArticleId_WhenUpdateArticleCount_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateArticleCount/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateArticleCountDto
        {
            Id = Guid.NewGuid()
        };
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
            System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenInvalidArticleId_WhenUpdateArticleLikes_ShouldReturnUnprocessableEntityt()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateArticleLikes/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateArticleLikesDto
        {
            Id = Guid.NewGuid(),
            AddToLikes = 10
        };
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
            System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenInvalidArticleIdAndValidJwt_WhenUpdateArticleVisibility_ShouldReturnForbidden()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateArticleVisibility/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);

        var payLoad = new UpdateArticleVisibilityDto
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
            System.Text.Encoding.Default, "application/json");
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ACCESS_DENIED);
    }

    [Fact]
    public async Task GivenInvalidArticleIdAndInvalidJwt_WhenUpdateArticleVisibility_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateArticleVisibility/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

        var payLoad = new UpdateArticleVisibilityDto
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
            System.Text.Encoding.Default, "application/json");
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }
}
