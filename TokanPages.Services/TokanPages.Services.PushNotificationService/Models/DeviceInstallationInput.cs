using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class DeviceInstallationInput
{
    /// <summary>
    /// Globally unique identifier string.
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    public string InstallationId { get; set; } = "";

    /// <summary>
    /// Custom string containing a combination of alphanumeric characters and -_@#.:=.
    /// There is a one to many relationship between UserID and Installation ID
    /// (i.e one User ID can be associated with multiple installations).
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public string? UserId { get; set; }

    /// <summary>
    /// The date when the installation was made inactivate by the PNS.
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public string? LastActiveOn { get; set; }

    /// <summary>
    /// A string containing the date and time in W3C DTF, YYYY-MM-DDThh:mmTZD (for example, 1997-07-16T19:20+01:00))
    /// in which the registration will expire. The value can be set at the hub level on create or update,
    /// and will default to never expire (9999-12-31T23:59:59).
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public string? ExpirationTime { get; set; }
    
    /// <summary>
    /// Date in W3C format of last update to this installation.
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public string? LastUpdate { get; set; }

    /// <summary>
    /// Can be {APNS, WNS, MPNS, ADM, GCM}.
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    public string Platform { get; set; } = "";

    /// <summary>
    /// The PNS handle for this installation (in case of WNS the ChannelUri of the ApplicationTile).
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    public string Handle { get; set; } = "";

    /// <summary>
    /// This is true if the PNS expired the channel.
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    public bool ExpiredPushChannel { get; set; }

    /// <summary>
    /// An array of tags. Tags are strings as defined in hub specs.
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public string[]? Tags { get; set; }

    /// <summary>
    /// A JSON object representing a dictionary of templateNames to template description.
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public JObject? Templates { get; set; }

    /// <summary>
    /// JSON object containing a dictionary of tileId and secondaryTiles objects.
    /// </summary>
    /// <remarks>
    /// Optional field.
    /// </remarks>
    public JObject? SecondaryTiles { get; set; }
}