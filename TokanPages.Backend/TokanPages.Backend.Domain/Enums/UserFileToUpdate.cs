using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum UserFileToUpdate
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
}