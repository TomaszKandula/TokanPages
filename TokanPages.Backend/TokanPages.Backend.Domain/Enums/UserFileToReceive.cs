using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum UserFileToReceive
{
    [EnumMember(Value = "image")]
    Image = 201,

    [EnumMember(Value = "audio")]
    Audio = 202,

    [EnumMember(Value = "video")]
    Video = 203,

    [EnumMember(Value = "document")]
    Document = 204,

    [EnumMember(Value = "application")]
    Application = 205,

    [EnumMember(Value = "any")]
    Any = 206,
}