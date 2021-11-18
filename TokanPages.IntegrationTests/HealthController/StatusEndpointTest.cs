namespace TokanPages.IntegrationTests.HealthController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System.Net;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Backend.Shared.Models;

    public class StatusEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/health";

        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public StatusEndpointTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

        [Fact]
        public async Task GivenCorrectConfiguration_WhenRequestStatusCheck_ShouldReturnSuccessful()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/Status/";
        
            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);
        
            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            
            var deserialized = JsonConvert.DeserializeObject<ActionResult>(content);
            deserialized.Should().NotBeNull();
            deserialized?.IsSucceeded.Should().BeTrue();
            deserialized?.ErrorCode.Should().BeNull();
            deserialized?.ErrorDesc.Should().BeNull();
        }

        [Fact]
        public async Task GivenInvalidDatabaseServer_WhenRequestStatusCheck_ShouldThrowError()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/Status/";
            var webAppFactory = _webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    configurationBuilder.AddInMemoryCollection(
                        new Dictionary<string, string> { ["ConnectionStrings:DbConnectTest"] = DataUtilityService.GetRandomString() });
                });
            });
            
            // Act
            var httpClient = webAppFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.InternalServerError);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            
            var deserialized = JsonConvert.DeserializeObject<ActionResult>(content);
            deserialized.Should().NotBeNull();
            deserialized?.IsSucceeded.Should().BeFalse();
            deserialized?.ErrorCode.Should().NotBeEmpty();
            deserialized?.ErrorDesc.Should().NotBeEmpty();
        }
    }
}