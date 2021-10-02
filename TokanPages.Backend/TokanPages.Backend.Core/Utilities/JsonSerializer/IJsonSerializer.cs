namespace TokanPages.Backend.Core.Utilities.JsonSerializer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public interface IJsonSerializer
    {
        string Serialize(object AModel, JsonSerializerSettings ASerializerSettings = null);

        T Deserialize<T>(string AJson, JsonSerializerSettings ASerializerSettings = null);

        JToken Parse(string AJson);
    }
}