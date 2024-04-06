using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

public enum PaymentStatuses
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,
    [EnumMember(Value = "unpaid")]
    Unpaid = 1,
    [EnumMember(Value = "partially paid")]
    PartiallyPaid = 2,
    [EnumMember(Value = "paid")]
    Paid = 3
}