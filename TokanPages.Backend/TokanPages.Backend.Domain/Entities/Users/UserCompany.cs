using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserCompanies")]
public class UserCompany : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required string CompanyName { get; set; }

    public required string VatNumber { get; set; }

    public required string EmailAddress { get; set; }

    public required string PhoneNumber { get; set; }

    public required string StreetAddress { get; set; }

    public required string PostalCode { get; set; }

    public required string City { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }

    public required CountryCode CountryCode { get; set; }
}