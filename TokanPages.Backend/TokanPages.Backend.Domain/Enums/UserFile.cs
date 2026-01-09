using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum UserFile
{
    [EnumMember(Value = "image")]
    Image = 0,

    [EnumMember(Value = "video")]
    Video = 1,

    [EnumMember(Value = "other")]
    Other = 2,
}