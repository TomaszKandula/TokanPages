using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

/// <summary>
/// Country codes. Numeric values follows UN Code.
/// </summary>
/// <remarks>
/// This is a incomplete list of country ISO codes as described in the ISO 3166 international standard.
/// In case of extending this list, please keep the correct numeric value.
/// </remarks>
public enum CountryCodes
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,
    [EnumMember(Value = "austria")]
    Austria = 40,
    [EnumMember(Value = "belgium")]
    Belgium = 56,
    [EnumMember(Value = "bulgaria")]
    Bulgaria = 100,
    [EnumMember(Value = "croatia")]
    Croatia = 191,
    [EnumMember(Value = "cyprus")]
    Cyprus = 196,
    [EnumMember(Value = "czech")]
    Czech = 203,
    [EnumMember(Value = "denmark")]
    Denmark = 208,
    [EnumMember(Value = "estonia")]
    Estonia = 233,
    [EnumMember(Value = "finland")]
    Finland = 246,
    [EnumMember(Value = "france")]
    France = 250,
    [EnumMember(Value = "germany")]
    Germany = 276,
    [EnumMember(Value = "greece")]
    Greece = 300,
    [EnumMember(Value = "hungary")]
    Hungary = 348,
    [EnumMember(Value = "ireland")]
    Ireland = 372,
    [EnumMember(Value = "italy")]
    Italy = 380,
    [EnumMember(Value = "latvia")]
    Latvia = 428,
    [EnumMember(Value = "lithuania")]
    Lithuania = 440,
    [EnumMember(Value = "luxembourg")]
    Luxembourg = 442,
    [EnumMember(Value = "malta")]
    Malta = 470,
    [EnumMember(Value = "netherlands")]
    Netherlands = 528,
    [EnumMember(Value = "poland")]
    Poland = 616,
    [EnumMember(Value = "portugal")]
    Portugal = 620,
    [EnumMember(Value = "romania")]
    Romania = 642,
    [EnumMember(Value = "slovakia")]
    Slovakia = 703,
    [EnumMember(Value = "slovenia")]
    Slovenia = 705,
    [EnumMember(Value = "spain")]
    Spain = 724,
    [EnumMember(Value = "sweden")]
    Sweden = 752,
    [EnumMember(Value = "usa")]
    Usa = 840,
    [EnumMember(Value = "china")]
    China = 156
}