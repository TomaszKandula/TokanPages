using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TokanPages.Tests.UnitTests.Models;
using Xunit;
using JsonSerializer = TokanPages.Backend.Core.Utilities.JsonSerializer.JsonSerializer;

namespace TokanPages.Tests.UnitTests.Services;

public class JsonSerializerTest : TestBase
{
    [Fact]
    public void GivenValidSimpleObject_WhenInvokeSerialize_ShouldSucceed()
    {
        // Arrange
        var validJsonString = GetValidJsonString();
        var testObject = new TestSimpleClass
        {
            Key1 = "Value1",
            Key2 = "Value2"
        };

        var jsonSerializer = new JsonSerializer();

        // Act
        var result = jsonSerializer.Serialize(testObject);

        // Assert
        result.Should().Be(validJsonString);
    }

    [Fact]
    public void GivenValidComplexObject_WhenInvokeSerialize_ShouldSucceed()
    {
        // Arrange
        var expected = GetFormattedValidJson();
        var testObject = new TestComplexClass
        {
            Key1 = "Value1",
            Key2 = "Value2",
            Languages = new List<TestLanguageClass>
            {
                new()
                {
                   LanguageCode = "pl-PL",
                   LanguageName = "Polski"
                },
                new()
                {
                    LanguageCode = "en-GB",
                    LanguageName = "British English"
                }
            }
        };

        var jsonSerializer = new JsonSerializer();

        // Act
        var result = jsonSerializer.Serialize(testObject, Formatting.Indented);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GivenValidString_WhenInvokeDeserialize_ShouldSucceed()
    {
        // Arrange
        var validJsonString = GetValidJsonString();
        var jsonSerializer = new JsonSerializer();

        // Act
        var result = jsonSerializer.Deserialize<TestSimpleClass>(validJsonString);

        // Assert
        result.Should().BeOfType<TestSimpleClass>();
        result.Key1.Should().Be("Value1");
        result.Key2.Should().Be("Value2");
    }

    [Fact]
    public void GivenEmptyString_WhenInvokeDeserialize_ShouldThrowError()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => jsonSerializer.Deserialize<TestSimpleClass>(string.Empty));
    }

    [Fact]
    public void GivenInvalidString_WhenInvokeDeserialize_ShouldThrowError()
    {
        // Arrange
        var invalidJsonString = GetInvalidJsonString();
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<JsonReaderException>(() => jsonSerializer.Deserialize<TestSimpleClass>(invalidJsonString));
    }

    [Fact]
    public void GivenValidJsonString_WhenParse_ShouldReturnJObject()
    {
        // Arrange
        var validJsonString = GetValidJsonString();
        var jsonSerializer = new JsonSerializer();

        // Act
        var parsed = jsonSerializer.Parse(validJsonString);

        // Assert
        parsed.Should().BeOfType<JObject>();
    }

    [Fact]
    public void GivenInvalidJsonString_WhenParse_ShouldThrowError()
    {
        // Arrange
        var invalidJsonString = GetInvalidJsonString();
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<JsonReaderException>(() => jsonSerializer.Parse(invalidJsonString));
    }

    [Fact]
    public void GivenEmptyJsonString_WhenParse_ShouldReturnNull()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => jsonSerializer.Parse(string.Empty));
    }

    [Fact]
    public void GivenParsedJsonList_WhenInvokeMapObjects_ShouldReturnListOfObjects()
    {
        // Arrange
        var validJsonList = GetValidJsonList();
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(validJsonList);

        // Act
        var result = jsonSerializer.MapObjects<TestSimpleClass>(parsed);

        // Assert
        var testClasses = result.ToList();
        testClasses.Should().HaveCount(2);
    }

    [Fact]
    public void GivenParsedJsonObject_WhenInvokeMapObjects_ShouldReturnEmptyList()
    {
        // Arrange
        var validJsonString = GetValidJsonString();
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(validJsonString);

        // Act
        var result = jsonSerializer.MapObjects<TestSimpleClass>(parsed);

        // Assert
        var testClasses = result.ToList();
        testClasses.Should().HaveCount(0);
    }

    [Fact]
    public void GivenParsedJsonObject_WhenInvokeMapObject_ShouldSucceed()
    {
        // Arrange
        var validJsonString = GetValidJsonString();
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(validJsonString);

        // Act
        var result = jsonSerializer.MapObject<TestSimpleClass>(parsed);

        // Assert
        result.Key1.Should().Be("Value1");
        result.Key2.Should().Be("Value2");
    }

    [Fact]
    public void GivenParsedJsonList_WhenInvokeMapObject_ShouldReturnEmptyObject()
    {
        // Arrange
        var validJsonList = GetValidJsonList();
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(validJsonList);

        // Act
        var result = jsonSerializer.MapObject<TestSimpleClass>(parsed);

        // Assert
        result.Key1.Should().BeNull();
        result.Key2.Should().BeNull();
    }

    private const string Directory = "Resources";

    private static string GetFormattedValidJson()
    {
        const string file = "FormattedValidJson.json";
        return File.ReadAllText(Path.Combine(Directory, file));
    }

    private static string GetInvalidJsonString()
    {
        const string file = "InvalidJsonString.json";
        return File.ReadAllText(Path.Combine(Directory, file));
    }
    
    private static string GetValidJsonList()
    {
        const string file = "ValidJsonList.json";
        return File.ReadAllText(Path.Combine(Directory, file));
    }

    private static string GetValidJsonString()
    {
        const string file = "ValidJsonString.json";
        return File.ReadAllText(Path.Combine(Directory, file));
    }
}