namespace TokanPages.Backend.Core.Utilities.JsonSerializer;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public interface IJsonSerializer
{
    string Serialize(object model, JsonSerializerSettings serializerSettings = null);

    T Deserialize<T>(string json, JsonSerializerSettings serializerSettings = null);

    JToken Parse(string json);

    IEnumerable<T> MapObjects<T>(JToken component) where T : new();

    T MapObject<T>(JToken component) where T : new();
}