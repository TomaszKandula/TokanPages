using System.Runtime.Serialization;

namespace TokanPages.Backend.Domain.Enums;

/// <summary>
/// Country currency codes (ISO 3166 and ISO 4217).
/// </summary>
/// <remarks>
/// The alphabetic code is based on another ISO standard, ISO 3166, which lists the codes for country names.
/// The first two letters of the ISO 4217 three-letter code are the same as the code for the country name, and,
/// where possible, the third letter corresponds to the first letter of the currency name.
/// The three-digit numeric code is useful when currency codes need to be understood in countries
/// that do not use Latin scripts and for computerized systems. Where possible,
/// the three-digit numeric code is the same as the numeric country code.
/// </remarks>
public enum CurrencyCodes
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,

    [EnumMember(Value = "bgn")]
    Bgn = 975,

    [EnumMember(Value = "chf")]
    Chf = 756,

    [EnumMember(Value = "czk")]
    Czk = 203,

    [EnumMember(Value = "dkk")]
    Dkk = 208,

    [EnumMember(Value = "eur")]
    Eur = 978,

    [EnumMember(Value = "gdp")]
    Gbp = 826,

    [EnumMember(Value = "hrk")]
    Hrk = 191,

    [EnumMember(Value = "huf")]
    Huf = 348,

    [EnumMember(Value = "nok")]
    Nok = 578,

    [EnumMember(Value = "pln")]
    Pln = 985,

    [EnumMember(Value = "ron")]
    Ron = 946,

    [EnumMember(Value = "sek")]
    Sek = 752,

    [EnumMember(Value = "try")]
    Try = 949,

    [EnumMember(Value = "usd")]
    Usd = 840
}