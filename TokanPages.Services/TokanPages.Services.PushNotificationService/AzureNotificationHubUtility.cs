using System.Security.Cryptography;
using System.Text;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Models;
using TokanPages.Services.PushNotificationService.Models.AppleNotificationService;
using TokanPages.Services.PushNotificationService.Models.FirebaseCloudMessaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TokanPages.Services.PushNotificationService;

public class AzureNotificationHubUtility : IAzureNotificationHubUtility
{
    private const string Scheme = "https";

    private const string Endpoint = "Endpoint";

    private const string SharedAccessKeyName = "SharedAccessKeyName";

    private const string SharedAccessKey = "SharedAccessKey";

    private readonly IJsonSerializer _jsonSerializer;

    public AzureNotificationHubUtility(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public ConnectionStringData GetConnectionStringData(string connectionString)
    {
        char[] separator = { ';' };
        var parts = connectionString.Split(separator);
        var output = new ConnectionStringData();

        foreach (var part in parts)
        {
            if (part.StartsWith(Endpoint))
                output.Endpoint = Scheme + part[11..];
            
            if (part.StartsWith(SharedAccessKeyName))
                output.SasKeyName = part[20..];
            
            if (part.StartsWith(SharedAccessKey))
                output.SasKeyValue = part[16..];
        }

        return output;
    }

    public string GetSaSToken(TokenInput input)
    {
        var targetUri = Uri.EscapeDataString(input.Uri.ToLower()).ToLower();

        var expiresOnDate = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        expiresOnDate += input.MinUntilExpire * 60 * 1000;

        var expiresSeconds = expiresOnDate / 1000;
        var toSign = targetUri + "\n" + expiresSeconds;

        var secretKeyByteArray = Convert.FromBase64String(input.SasKeyValue);
        var signatureByteArray = Encoding.UTF8.GetBytes(toSign);

        using var hmac = new HMACSHA256(secretKeyByteArray);
        var signatureBytes = hmac.ComputeHash(signatureByteArray);
        var signatureBase64 = Convert.ToBase64String(signatureBytes);

        return $"SharedAccessSignature sr={targetUri}&sig={signatureBase64}&se={expiresSeconds}&skn={input.SasKeyName}";
    }

    public string MakeApsMessage(MessageInput input)
    {
        var model = new MessageModelAps
        {
            Aps = new Aps
            {
                Alert = new Alert
                {
                    Title = input.Title,
                    Body = input.Body
                }
            }
        };

        var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        return _jsonSerializer.Serialize(model, Formatting.None, settings);
    }

    public string MakeFcmMessage(MessageInput input)
    {
        var model = new MessageModelFcm
        {
            Notification = new Notification
            {
                Title = input.Title,
                Body = input.Body
            }
        };

        var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        return _jsonSerializer.Serialize(model, Formatting.None, settings);
    }
}