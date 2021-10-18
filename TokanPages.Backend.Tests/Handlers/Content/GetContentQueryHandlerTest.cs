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
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var content = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var contentType = MediaTypeHeaderValue.Parse("application/json");
            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Content = content
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            var result = await getContentQueryHandler.Handle(getContentQuery, CancellationToken.None);

            // Assert
            result.ContentName.Should().Be("activateAccount");
            result.ContentType.Should().Be("component");

            var activateAccountDto = result.Content as ActivateAccountDto;
            activateAccountDto.Should().NotBeNull();
            
            activateAccountDto?.OnProcessing.Type.Should().Be(testObject[0].OnProcessing.Type);
            activateAccountDto?.OnProcessing.Caption.Should().Be(testObject[0].OnProcessing.Caption);
            activateAccountDto?.OnProcessing.Text1.Should().Be(testObject[0].OnProcessing.Text1);
            activateAccountDto?.OnProcessing.Text2.Should().Be(testObject[0].OnProcessing.Text2);
            activateAccountDto?.OnProcessing.Button.Should().Be(testObject[0].OnProcessing.Button);

            activateAccountDto?.OnSuccess.Type.Should().Be(testObject[0].OnSuccess.Type);
            activateAccountDto?.OnSuccess.Caption.Should().Be(testObject[0].OnSuccess.Caption);
            activateAccountDto?.OnSuccess.Text1.Should().Be(testObject[0].OnSuccess.Text1);
            activateAccountDto?.OnSuccess.Text2.Should().Be(testObject[0].OnSuccess.Text2);
            activateAccountDto?.OnSuccess.Button.Should().Be(testObject[0].OnSuccess.Button);

            activateAccountDto?.OnError.Type.Should().Be(testObject[0].OnError.Type);
            activateAccountDto?.OnError.Caption.Should().Be(testObject[0].OnError.Caption);
            activateAccountDto?.OnError.Text1.Should().Be(testObject[0].OnError.Text1);
            activateAccountDto?.OnError.Text2.Should().Be(testObject[0].OnError.Text2);
            activateAccountDto?.OnError.Button.Should().Be(testObject[0].OnError.Button);
        }

        [Fact]
        public async Task GivenValidComponentNameAndTypeAndNonExistingLanguage_WhenGetContent_ShouldReturnNoContent()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component",
                Language = "pl" 
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var content = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var contentType = MediaTypeHeaderValue.Parse("application/json");
            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Content = content
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            var result = await getContentQueryHandler.Handle(getContentQuery, CancellationToken.None);

            // Assert
            result.ContentName.Should().Be("activateAccount");
            result.ContentType.Should().Be("component");

            var activateAccountDto = result.Content as ActivateAccountDto;
            activateAccountDto.Should().BeNull();
        }

        [Fact]
        public async Task GivenInvalidComponentType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = DataUtilityService.GetRandomString()
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var content = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var contentType = MediaTypeHeaderValue.Parse("application/json");
            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Content = content
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => getContentQueryHandler.Handle(getContentQuery, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED));
        }

        [Fact]
        public async Task GivenEmptyContentFromRemoteService_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var contentType = MediaTypeHeaderValue.Parse("application/json");
            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Content = null
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => getContentQueryHandler.Handle(getContentQuery, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY));
        }

        [Fact]
        public async Task GivenInvalidRemoteComponent_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var content = await new StringContent(DataUtilityService.GetRandomString()).ReadAsByteArrayAsync();
            var contentType = MediaTypeHeaderValue.Parse("application/json");
            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Content = content
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new InvalidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => getContentQueryHandler.Handle(getContentQuery, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN));
        }

        [Fact]
        public async Task GivenInvalidRemoteServiceResponse_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Name = "activateAccount",
                Type = "component"
            };

            var mockedCustomHttpClient = new Mock<ICustomHttpClient>();
            var mockedJsonSerializer = new Mock<IJsonSerializer>();
            var mockedAzureStorage = new Mock<AzureStorage>();

            var contentResult = new Results
            {
                StatusCode = HttpStatusCode.BadRequest,
                ContentType = null,
                Content = null
            };

            var testObject = GetActivateAccountContent();
            var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
            var testJObject = JObject.FromObject(activateAccountObject);

            mockedCustomHttpClient
                .Setup(client => client.Execute(
                    It.IsAny<Configuration>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentResult);

            mockedJsonSerializer
                .Setup(serializer => serializer.Parse(It.IsAny<string>()))
                .Returns(testJObject);

            mockedJsonSerializer
                .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
                .Returns(testObject);

            var getContentQueryHandler = new GetContentQueryHandler(
                mockedCustomHttpClient.Object,
                mockedJsonSerializer.Object,
                mockedAzureStorage.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() => getContentQueryHandler.Handle(getContentQuery, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_NOT_FOUND));
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