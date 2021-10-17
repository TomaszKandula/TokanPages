namespace TokanPages.Backend.Core.Utilities.JsonSerializer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object model, JsonSerializerSettings serializerSettings = null)
            => JsonConvert.SerializeObject(model, serializerSettings);

        public T Deserialize<T>(string json, JsonSerializerSettings serializerSettings = null)
            => string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json, serializerSettings);

        public JToken Parse(string json)
            => string.IsNullOrEmpty(json) ? default : JToken.Parse(json);

        public IEnumerable<T> MapObjects<T>(JToken component) where T : new()
        {
            return component switch
            {
                JArray => component.ToObject<IEnumerable<T>>(),
                _ => new List<T>()
            };
        }

        public T MapObject<T>(JToken component) where T : new()
        {
            return component switch
            {
                JObject => component.ToObject<T>(),
                _ => new T()
            };
        }
    }
}