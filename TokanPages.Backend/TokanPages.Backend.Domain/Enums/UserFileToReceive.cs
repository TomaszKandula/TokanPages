using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum UserFileToReceive
{
    [EnumMember(Value = "image")]
    Image = 0,

    [EnumMember(Value = "audio")]
    Audio = 1,

    [EnumMember(Value = "video")]
    Video = 2,

    [EnumMember(Value = "document")]
    Document = 3,

    [EnumMember(Value = "application")]
    Application = 4,

    [EnumMember(Value = "any")]
    Any = 5,
}