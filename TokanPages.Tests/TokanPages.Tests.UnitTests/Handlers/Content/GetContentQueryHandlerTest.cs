using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TokanPages.Backend.Application.Content.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Factory;
using TokanPages.Services.AzureStorageService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Content;

public class GetContentQueryHandlerTest : TestBase
{
    // [Fact] TODO: align test to changed implementation
    // public async Task GivenValidComponentNameAndType_WhenGetContent_ShouldSucceed()
    // {
    //     // Arrange
    //     var query = new GetContentQuery
    //     {
    //         Name = "activateAccount",
    //         Type = "component"
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedJsonSerializer = new Mock<IJsonSerializer>();
    //     var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
    //     var mockedAzureBlob = new Mock<IAzureBlobStorage>();
    //
    //     var streamContent = new StorageStreamContent
    //     {
    //         Content = DataUtilityService.GetRandomStream(),
    //         ContentType = "application/json"
    //     };
    //
    //     mockedAzureBlob
    //         .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(streamContent);
    //     
    //     mockedAzureStorage
    //         .Setup(factory => factory.Create())
    //         .Returns(mockedAzureBlob.Object);
    //
    //     var testObject = GetActivateAccountContent();
    //     var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
    //     var testJObject = JObject.FromObject(activateAccountObject);
    //
    //     mockedJsonSerializer
    //         .Setup(serializer => serializer.Parse(It.IsAny<string>()))
    //         .Returns(testJObject);
    //
    //     mockedJsonSerializer
    //         .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
    //         .Returns(testObject);
    //
    //     var handler = new GetContentQueryHandler(
    //         databaseContext,
    //         mockedLogger.Object,
    //         mockedJsonSerializer.Object,
    //         mockedAzureStorage.Object);
    //
    //     // Act
    //     var result = await handler.Handle(query, CancellationToken.None);
    //
    //     // Assert
    //     result.ContentName.Should().Be("activateAccount");
    //     result.ContentType.Should().Be("component");
    //
    //     var activateAccountDto = result.Content as ActivateAccountDto;
    //     activateAccountDto.Should().NotBeNull();
    //         
    //     activateAccountDto?.OnProcessing.Type.Should().Be(testObject[0].OnProcessing.Type);
    //     activateAccountDto?.OnProcessing.Caption.Should().Be(testObject[0].OnProcessing.Caption);
    //     activateAccountDto?.OnProcessing.Text1.Should().Be(testObject[0].OnProcessing.Text1);
    //     activateAccountDto?.OnProcessing.Text2.Should().Be(testObject[0].OnProcessing.Text2);
    //     activateAccountDto?.OnProcessing.Button.Should().Be(testObject[0].OnProcessing.Button);
    //
    //     activateAccountDto?.OnSuccess.Type.Should().Be(testObject[0].OnSuccess.Type);
    //     activateAccountDto?.OnSuccess.Caption.Should().Be(testObject[0].OnSuccess.Caption);
    //     activateAccountDto?.OnSuccess.Text1.Should().Be(testObject[0].OnSuccess.Text1);
    //     activateAccountDto?.OnSuccess.Text2.Should().Be(testObject[0].OnSuccess.Text2);
    //     activateAccountDto?.OnSuccess.Button.Should().Be(testObject[0].OnSuccess.Button);
    //
    //     activateAccountDto?.OnError.Type.Should().Be(testObject[0].OnError.Type);
    //     activateAccountDto?.OnError.Caption.Should().Be(testObject[0].OnError.Caption);
    //     activateAccountDto?.OnError.Text1.Should().Be(testObject[0].OnError.Text1);
    //     activateAccountDto?.OnError.Text2.Should().Be(testObject[0].OnError.Text2);
    //     activateAccountDto?.OnError.Button.Should().Be(testObject[0].OnError.Button);
    // }
    //
    // [Fact] TODO: align test to changed implementation
    // public async Task GivenValidComponentNameAndTypeAndNonExistingLanguage_WhenGetContent_ShouldReturnNoContent()
    // {
    //     // Arrange
    //     var query = new GetContentQuery
    //     {
    //         Name = "activateAccount-eng",
    //         Type = "component",
    //         Language = "pol" 
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedJsonSerializer = new Mock<IJsonSerializer>();
    //     var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
    //     var mockedAzureBlob = new Mock<IAzureBlobStorage>();
    //
    //     var streamContent = new StorageStreamContent
    //     {
    //         Content = DataUtilityService.GetRandomStream(),
    //         ContentType = "application/json"
    //     };
    //
    //     mockedAzureBlob
    //         .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(streamContent);
    //
    //     mockedAzureStorage
    //         .Setup(factory => factory.Create())
    //         .Returns(mockedAzureBlob.Object);
    //
    //     var testObject = GetActivateAccountContent();
    //     var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
    //     var testJObject = JObject.FromObject(activateAccountObject);
    //
    //     mockedJsonSerializer
    //         .Setup(serializer => serializer.Parse(It.IsAny<string>()))
    //         .Returns(testJObject);
    //
    //     mockedJsonSerializer
    //         .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
    //         .Returns(testObject);
    //
    //     var handler = new GetContentQueryHandler(
    //         databaseContext,
    //         mockedLogger.Object,
    //         mockedJsonSerializer.Object,
    //         mockedAzureStorage.Object);
    //
    //     // Act
    //     var result = await handler.Handle(query, CancellationToken.None);
    //
    //     // Assert
    //     result.ContentName.Should().Be("activateAccount");
    //     result.ContentType.Should().Be("component");
    //
    //     var activateAccountDto = result.Content as ActivateAccountDto;
    //     activateAccountDto.Should().BeNull();
    // }

    [Fact]
    public async Task GivenInvalidComponentType_WhenGetContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetContentQuery
        {
            Name = "activateAccount",
            Type = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();

        var streamContent = new StorageStreamContent
        {
            Content = DataUtilityService.GetRandomStream(),
            ContentType = "application/json"
        };

        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(streamContent);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlob.Object);

        var testObject = GetActivateAccountContent();
        var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
        var testJObject = JObject.FromObject(activateAccountObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.Parse(It.IsAny<string>()))
            .Returns(testJObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
            .Returns(testObject);

        var handler = new GetContentQueryHandler(
            databaseContext,
            mockedLogger.Object,
            mockedJsonSerializer.Object,
            mockedAzureStorage.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED));
    }

    [Fact]
    public async Task GivenEmptyContentFromRemoteService_WhenGetContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetContentQuery
        {
            Name = "activateAccount",
            Type = "component"
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();

        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StorageStreamContent)null!);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlob.Object);

        var testObject = GetActivateAccountContent();
        var activateAccountObject = new ValidActivateAccountObject { ActivateAccount = testObject };
        var testJObject = JObject.FromObject(activateAccountObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.Parse(It.IsAny<string>()))
            .Returns(testJObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
            .Returns(testObject);

        var handler = new GetContentQueryHandler(
            databaseContext,
            mockedLogger.Object,
            mockedJsonSerializer.Object,
            mockedAzureStorage.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_NOT_FOUND));
    }

    [Fact]
    public async Task GivenInvalidRemoteComponent_WhenGetContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetContentQuery
        {
            Name = "activateAccount",
            Type = "component"
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedJsonSerializer = new Mock<IJsonSerializer>();
        var mockedAzureStorage = new Mock<IAzureBlobStorageFactory>();
        var mockedAzureBlob = new Mock<IAzureBlobStorage>();

        var streamContent = new StorageStreamContent
        {
            Content = DataUtilityService.GetRandomStream(),
            ContentType = "application/json"
        };

        mockedAzureBlob
            .Setup(storage => storage.OpenRead(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(streamContent);

        mockedAzureStorage
            .Setup(factory => factory.Create())
            .Returns(mockedAzureBlob.Object);

        var testObject = GetActivateAccountContent();
        var activateAccountObject = new InvalidActivateAccountObject { ActivateAccount = testObject };
        var testJObject = JObject.FromObject(activateAccountObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.Parse(It.IsAny<string>()))
            .Returns(testJObject);

        mockedJsonSerializer
            .Setup(serializer => serializer.MapObjects<ActivateAccountDto>(It.IsAny<JToken>()))
            .Returns(testObject);

        var handler = new GetContentQueryHandler(
            databaseContext,
            mockedLogger.Object,
            mockedJsonSerializer.Object,
            mockedAzureStorage.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN));
    }

    private static List<ActivateAccountDto> GetActivateAccountContent() => new ()
    {
        new ActivateAccountDto
        {
            Language = "eng",
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
        public List<ActivateAccountDto>? ActivateAccount { get; set; }
    }

    private class InvalidActivateAccountObject
    {
        [JsonProperty("AccountActivation")]
        public List<ActivateAccountDto>? ActivateAccount { get; set; }
    }

    private class ActivateAccountDto
    {
        [JsonProperty("language")]
        public string Language { get; set; } = "";

        [JsonProperty("onProcessing")]
        public ContentActivation OnProcessing { get; set; } = new();
        
        [JsonProperty("onSuccess")]
        public ContentActivation OnSuccess { get; set; } = new();
        
        [JsonProperty("onError")]
        public ContentActivation OnError { get; set; } = new();
    }

    private class ContentActivation
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "";
        
        /// <summary>
        /// Caption
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; } = "";
        
        /// <summary>
        /// Text1
        /// </summary>
        [JsonProperty("text1")]
        public string Text1 { get; set; } = "";
        
        /// <summary>
        /// Text2
        /// </summary>
        [JsonProperty("text2")]
        public string Text2 { get; set; } = "";
        
        /// <summary>
        /// Button
        /// </summary>
        [JsonProperty("button")]
        public string Button { get; set; } = "";
    }
}