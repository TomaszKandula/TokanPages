namespace TokanPages.WebApi.Tests.ContentController
{
    using Xunit;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Content;

    public partial class ContentControllerTest
    {
        [Fact]
        public async Task GivenComponentNameAndType_WhenGetContent_ShouldSucceed()
        {
            // Arrange
            const string COMPONENT_NAME = "activateAccount";
            const string COMPONENT_TYPE = "component";
            
            var LRequest = $"{API_BASE_URL}/GetContent/?AName={COMPONENT_NAME}&AType={COMPONENT_TYPE}";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetContentQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenInvalidComponentNameAndValidType_WhenGetContent_ShouldReturnBadRequest()
        {
            // Arrange
            var LComponentName = DataUtilityService.GetRandomString(10, "", true);
            const string COMPONENT_TYPE = "component";
            
            var LRequest = $"{API_BASE_URL}/GetContent/?AName={LComponentName}&AType={COMPONENT_TYPE}";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.COMPONENT_NOT_FOUND);
        }

        [Fact]
        public async Task GivenValidComponentNameAndInvalidType_WhenGetContent_ShouldReturnBadRequest()
        {
            // Arrange
            const string COMPONENT_NAME = "activateAccount";
            var LComponentType = DataUtilityService.GetRandomString(6, "", true);
            
            var LRequest = $"{API_BASE_URL}/GetContent/?AName={COMPONENT_NAME}&AType={LComponentType}";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);
        }
    }
}