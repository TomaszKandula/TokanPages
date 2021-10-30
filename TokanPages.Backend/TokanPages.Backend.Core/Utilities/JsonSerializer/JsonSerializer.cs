namespace TokanPages.Backend.Core.Utilities.JsonSerializer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSerializer : IJsonSerializer
    {
        public virtual string Serialize(object model, JsonSerializerSettings serializerSettings = null)
            => JsonConvert.SerializeObject(model, serializerSettings);

        public virtual T Deserialize<T>(string json, JsonSerializerSettings serializerSettings = null)
            => string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json, serializerSettings);

        public virtual JToken Parse(string json)
            => string.IsNullOrEmpty(json) ? default : JToken.Parse(json);

        public virtual IEnumerable<T> MapObjects<T>(JToken component) where T : new()
        {
            return component switch
            {
                JArray => component.ToObject<IEnumerable<T>>(),
                _ => new List<T>()
            };
        }

        public virtual T MapObject<T>(JToken component) where T : new()
        {
            return component switch
            {
                JObject => component.ToObject<T>(),
                _ => new T()
            };
        }
    }
}