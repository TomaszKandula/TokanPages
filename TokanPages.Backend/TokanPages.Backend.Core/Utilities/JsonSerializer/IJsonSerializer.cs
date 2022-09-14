using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TokanPages.Backend.Core.Utilities.JsonSerializer;

public interface IJsonSerializer
{
    string Serialize(object model, JsonSerializerSettings? serializerSettings = default);

    T Deserialize<T>(string json, JsonSerializerSettings? serializerSettings = default);

    JToken Parse(string json);

    IEnumerable<T> MapObjects<T>(JToken component) where T : new();

    T MapObject<T>(JToken component) where T : new();
}