using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum PaymentTypes
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,
    [EnumMember(Value = "credit card")]
    CreditCard = 1,
    [EnumMember(Value = "wire transfer")]
    WireTransfer = 2
}