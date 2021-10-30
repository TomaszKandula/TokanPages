namespace TokanPages.WebApi.Tests.MetricsController
{
    using Xunit;
    using FluentAssertions;
    using System.Net;
    using System.Threading.Tasks;

    public partial class MetricsControllerTest
    {
        [Theory]
        [InlineData("tokanpages-backend")]
        [InlineData("tokanpages-frontend")]
        public async Task GivenProjectName_WhenRequestQualityGate_ShouldReturnSvgFile(string project)
        {
            // Arrange
            var request = $"{ApiBaseUrl}/Quality/?Project={project}";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidProjectName_WhenRequestQualityGate_ShouldReturnSvgWithError()
        {
            // Arrange
            const string invalidProjectName = "InvalidProjectName"; 
            var request = $"{ApiBaseUrl}/Quality/?Project={invalidProjectName}";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Project has not been found");
        }

        [Fact]
        public async Task GivenEmptyProjectName_WhenRequestQualityGate_ShouldThrowError()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/Quality/?Project=";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.BadRequest);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Be("Parameter 'project' is missing");
        }
    }
}