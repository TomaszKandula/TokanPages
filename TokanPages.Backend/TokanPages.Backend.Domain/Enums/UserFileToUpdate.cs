using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum UserFileToUpdate
{
    [EnumMember(Value = "image")]
    Image = 101,

    [EnumMember(Value = "audio")]
    Audio = 102,

    [EnumMember(Value = "video")]
    Video = 103,

    [EnumMember(Value = "document")]
    Document = 104,

    [EnumMember(Value = "application")]
    Application = 105,
}