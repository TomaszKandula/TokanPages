using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database.Initializer.Data.Articles;
using TokanPages.Tests.EndToEndTests.Helpers;
using TokanPages.WebApi.Dto.Articles;
using Xunit;

namespace TokanPages.Tests.EndToEndTests.Controllers;

public class ArticlesControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _factory;

    public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> factory)
    {
        _factory = factory;
        ExternalDatabaseConnection = _factory.Connection;
    }

    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/GetAllArticles/?IsPublished=false";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<List<GetAllArticlesQueryResult>>(content);
        deserialized.Should().NotBeNullOrEmpty();
        deserialized.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
    {
        // Arrange
        var userId = Article1.Id;
        var uri = $"{BaseUriArticles}/{userId}/GetArticle/?noCache=true";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(content);
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        var uri = $"{BaseUriArticles}/{Guid.NewGuid()}/GetArticle/?noCache=true";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrectAsAnonymousUser_WhenAddArticle_ShouldReturnUnauthorized()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/AddArticle/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new AddArticleDto
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrectAsLoggedUser_WhenAddArticle_ShouldSucceed()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/AddArticle/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new AddArticleDto
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _factory.WebSecret, _factory.Issuer, _factory.Audience);

        await RegisterTestJwtInDatabase(jwt);

        var payload = JsonConvert.SerializeObject(dto);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnauthorized()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/RemoveArticle/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new RemoveArticleDto { Id = Guid.NewGuid() };
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenNoJwt_WhenUpdateArticleContent_ShouldReturnUnauthorized()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/UpdateArticleContent/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateArticleContentDto
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(150),
            ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenInvalidArticleId_WhenUpdateArticleCount_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/UpdateArticleCount/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateArticleCountDto { Id = Guid.NewGuid() };
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenInvalidArticleId_WhenUpdateArticleLikes_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/UpdateArticleLikes/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateArticleLikesDto
        {
            Id = Guid.NewGuid(),
            AddToLikes = 10
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenInvalidArticleIdAndValidJwt_WhenUpdateArticleVisibility_ShouldReturnForbidden()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/UpdateArticleVisibility/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _factory.WebSecret, _factory.Issuer, _factory.Audience);

        await RegisterTestJwtInDatabase(jwt);

        var dto = new UpdateArticleVisibilityDto
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };

        var payload = JsonConvert.SerializeObject(dto);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ACCESS_DENIED);
    }

    [Fact]
    public async Task GivenInvalidArticleIdAndInvalidJwt_WhenUpdateArticleVisibility_ShouldReturnUnauthorized()
    {
        // Arrange
        const string uri = $"{BaseUriArticles}/UpdateArticleVisibility/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _factory.WebSecret, _factory.Issuer, _factory.Audience);

        var dto = new UpdateArticleVisibilityDto
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };

        var payload = JsonConvert.SerializeObject(dto);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }
}
