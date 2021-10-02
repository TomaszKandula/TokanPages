namespace TokanPages.Backend.Core.Utilities.JsonSerializer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object AModel, JsonSerializerSettings ASerializerSettings = null)
            => JsonConvert.SerializeObject(AModel, ASerializerSettings);

        public T Deserialize<T>(string AJson, JsonSerializerSettings ASerializerSettings = null)
            => string.IsNullOrEmpty(AJson) ? default : JsonConvert.DeserializeObject<T>(AJson, ASerializerSettings);

        public JToken Parse(string AJson)
            => string.IsNullOrEmpty(AJson) ? default : JToken.Parse(AJson);
    }
}