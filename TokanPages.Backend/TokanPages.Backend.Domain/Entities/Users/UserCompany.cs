using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserCompanies")]
public class UserCompany : Entity<Guid>
{
    public Guid UserId { get; set; }

    public string CompanyName { get; set; }

    public string VatNumber { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public string StreetAddress { get; set; }

    public string PostalCode { get; set; }

    public string City { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public CountryCode CountryCode { get; set; }
}