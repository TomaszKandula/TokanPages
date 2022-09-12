using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TokanPages.Tests.IntegrationTests.Factories;
using TokanPages.WebApi.Dto.Health;
using Xunit;

namespace TokanPages.Tests.IntegrationTests.Controllers;

public class StatusControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public StatusControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task GivenCorrectConfiguration_WhenRequestStatusCheck_ShouldReturnSuccessful()
    {
        // Arrange
        const string uri = $"{BaseUriHeath}/Status/";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<ActionResultDto>(content);
        deserialized.Should().NotBeNull();
        deserialized?.IsSucceeded.Should().BeTrue();
        deserialized?.ErrorCode.Should().BeEmpty();
        deserialized?.ErrorDesc.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInvalidDatabaseServer_WhenRequestStatusCheck_ShouldThrowError()
    {
        // Arrange
        const string uri = $"{BaseUriHeath}/Status/";
        var webAppFactory = _webApplicationFactory.WithWebHostBuilder(builder =>
        {
            builder.UseSolutionRelativeContentRoot(TestRootPath);
            builder.ConfigureAppConfiguration((_, configurationBuilder) =>
            {
                configurationBuilder.AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        ["ConnectionStrings:DbConnectTest"] = DataUtilityService.GetRandomString()
                    });
            });
        });

        var httpClient = webAppFactory.CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.InternalServerError);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<ActionResultDto>(content);
        deserialized.Should().NotBeNull();
        deserialized?.IsSucceeded.Should().BeFalse();
        deserialized?.ErrorCode.Should().NotBeEmpty();
        deserialized?.ErrorDesc.Should().NotBeEmpty();
    }
}