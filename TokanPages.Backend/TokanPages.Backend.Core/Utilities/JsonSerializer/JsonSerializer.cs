using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Core.Utilities.JsonSerializer;

public class JsonSerializer : IJsonSerializer
{
    public virtual string Serialize(object model, JsonSerializerSettings? serializerSettings = default)
        => JsonConvert.SerializeObject(model, serializerSettings);

    public virtual T Deserialize<T>(string json, JsonSerializerSettings? serializerSettings = default)
    {
        if (string.IsNullOrEmpty(json))
            throw new ArgumentException(ErrorCodes.ARGUMENT_EMPTY_OR_NULL);

        var deserialized = JsonConvert.DeserializeObject<T>(json, serializerSettings);
        if (deserialized is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        return deserialized;
    }

    public virtual JToken Parse(string json)
    {
        if (string.IsNullOrEmpty(json))
            throw new ArgumentException(ErrorCodes.ARGUMENT_EMPTY_OR_NULL);

        var parsed = JToken.Parse(json);
        if (parsed is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        return parsed;
    }

    public virtual IEnumerable<T> MapObjects<T>(JToken component) where T : new()
    {
        return component switch
        {
            JArray => component.ToObject<IEnumerable<T>>() ?? new List<T>(),
            _ => new List<T>()
        };
    }

    public virtual T MapObject<T>(JToken component) where T : new()
    {
        return component switch
        {
            JObject => component.ToObject<T>() ?? new T(),
            _ => new T()
        };
    }
}