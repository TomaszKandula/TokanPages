namespace TokanPages.Backend.Core.Utilities.JsonSerializer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public interface IJsonSerializer
    {
        string Serialize(object AModel, JsonSerializerSettings ASerializerSettings = null);

        T Deserialize<T>(string AJson, JsonSerializerSettings ASerializerSettings = null);

        JToken Parse(string AJson);

        IEnumerable<T> MapObjects<T>(JToken AComponent) where T : new();

        T MapObject<T>(JToken AComponent) where T : new();
    }
}