namespace TokanPages.Backend.Tests.Handlers.Content
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Dto.Content;
    using Shared.Dto.Content.Common;
    using Cqrs.Handlers.Queries.Content;
    using Core.Utilities.JsonSerializer;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class GetContentQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidComponentNameAndType_WhenGetContent_ShouldSucceed()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContent = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var LContentType = MediaTypeHeaderValue.Parse("application/json");
            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = LContentType,
                Content = LContent
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new ValidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            var LResult = await LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None);

            // Assert
            LResult.ContentName.Should().Be("activateAccount");
            LResult.ContentType.Should().Be("component");

            var LActivateAccountDto = LResult.Content as ActivateAccountDto;
            LActivateAccountDto.Should().NotBeNull();
            
            LActivateAccountDto?.OnProcessing.Type.Should().Be(LTestObject[0].OnProcessing.Type);
            LActivateAccountDto?.OnProcessing.Caption.Should().Be(LTestObject[0].OnProcessing.Caption);
            LActivateAccountDto?.OnProcessing.Text1.Should().Be(LTestObject[0].OnProcessing.Text1);
            LActivateAccountDto?.OnProcessing.Text2.Should().Be(LTestObject[0].OnProcessing.Text2);
            LActivateAccountDto?.OnProcessing.Button.Should().Be(LTestObject[0].OnProcessing.Button);

            LActivateAccountDto?.OnSuccess.Type.Should().Be(LTestObject[0].OnSuccess.Type);
            LActivateAccountDto?.OnSuccess.Caption.Should().Be(LTestObject[0].OnSuccess.Caption);
            LActivateAccountDto?.OnSuccess.Text1.Should().Be(LTestObject[0].OnSuccess.Text1);
            LActivateAccountDto?.OnSuccess.Text2.Should().Be(LTestObject[0].OnSuccess.Text2);
            LActivateAccountDto?.OnSuccess.Button.Should().Be(LTestObject[0].OnSuccess.Button);

            LActivateAccountDto?.OnError.Type.Should().Be(LTestObject[0].OnError.Type);
            LActivateAccountDto?.OnError.Caption.Should().Be(LTestObject[0].OnError.Caption);
            LActivateAccountDto?.OnError.Text1.Should().Be(LTestObject[0].OnError.Text1);
            LActivateAccountDto?.OnError.Text2.Should().Be(LTestObject[0].OnError.Text2);
            LActivateAccountDto?.OnError.Button.Should().Be(LTestObject[0].OnError.Button);
        }

        [Fact]
        public async Task GivenValidComponentNameAndTypeAndNonExistingLanguage_WhenGetContent_ShouldReturnNoContent()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component",
                Language = "pl" 
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContent = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var LContentType = MediaTypeHeaderValue.Parse("application/json");
            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = LContentType,
                Content = LContent
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new ValidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            var LResult = await LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None);

            // Assert
            LResult.ContentName.Should().Be("activateAccount");
            LResult.ContentType.Should().Be("component");

            var LActivateAccountDto = LResult.Content as ActivateAccountDto;
            LActivateAccountDto.Should().BeNull();
        }

        [Fact]
        public async Task GivenInvalidComponentType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = DataUtilityService.GetRandomString()
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContent = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var LContentType = MediaTypeHeaderValue.Parse("application/json");
            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = LContentType,
                Content = LContent
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new ValidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED));
        }

        [Fact]
        public async Task GivenEmptyContentFromRemoteService_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContentType = MediaTypeHeaderValue.Parse("application/json");
            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = LContentType,
                Content = null
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new ValidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY));
        }

        [Fact]
        public async Task GivenInvalidRemoteComponent_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContent = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var LContentType = MediaTypeHeaderValue.Parse("application/json");
            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = LContentType,
                Content = LContent
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new InvalidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN));
        }

        [Fact]
        public async Task GivenInvalidRemoteServiceResponse_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var LMockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var LMockedJsonSerializer = new Mock<IJsonSerializer>();
            var LMockedAzureStorage = new Mock<AzureStorage>();

            var LContentResult = new Results
            {
                StatusCode = HttpStatusCode.BadRequest,
                ContentType = null,
                Content = null
            };

            var LTestObject = GetActivateAccountContent();
            var LActivateAccountObject = new ValidActivateAccountObject { ActivateAccount = LTestObject };
            var LTestJObject = JObject.FromObject(LActivateAccountObject);

            LMockedCustomHttpClient
                .Setup(AClient => AClient.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(LContentResult);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.Parse(It.IsAny<string>()))
                .Returns(LTestJObject);

            LMockedJsonSerializer
                .Setup(ASerializer => ASerializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(LTestObject);

            var LGetContentQueryHandler = new GetContentQueryHandler(
                LMockedCustomHttpClient.Object,
                LMockedJsonSerializer.Object,
                LMockedAzureStorage.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LGetContentQueryHandler.Handle(LGetContentQuery, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
        }

        private static List<ActivateAccountDto> GetActivateAccountContent() => new ()
        {
            new ActivateAccountDto
            {
                Language = "en",
                OnProcessing = new ContentActivation
                {
                    Type = "Processing",
                    Caption = "Account Activation",
                    Text1 = "Processing your account..., please wait.",
                    Text2 = "",
                    Button = ""
                },
                OnSuccess = new ContentActivation
                {
                    Type = "Success",
                    Caption = "Account Activation",
                    Text1 = "Your account has been successfully activated!",
                    Text2 = "You can now sign in.",
                    Button = "Go to main"
                }, 
                OnError = new ContentActivation
                {
                    Type = "Error",
                    Caption = "Account Activation",
                    Text1 = "Could not activate your account.",
                    Text2 = "Please contact IT support.",
                    Button = "Retry" 
                }
            }
        };

        private class ValidActivateAccountObject
        {
            [JsonProperty("activateAccount")]
            public List<ActivateAccountDto> ActivateAccount { get; set; }
        }

        private class InvalidActivateAccountObject
        {
            [JsonProperty("AccountActivation")]
            public List<ActivateAccountDto> ActivateAccount { get; set; }
        }
    }
}