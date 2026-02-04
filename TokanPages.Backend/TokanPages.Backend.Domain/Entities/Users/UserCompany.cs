using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserCompanies")]
public class UserCompany : Entity<Guid>
{
    public Guid UserId { get; set; }
    [MaxLength(255)]
    public string CompanyName { get; set; }
    [MaxLength(25)]
    public string VatNumber { get; set; }
    [MaxLength(255)]
    public string EmailAddress { get; set; }
    [MaxLength(11)]
    public string PhoneNumber { get; set; }
    [Required]
    [MaxLength(255)]
    public string StreetAddress { get; set; }
    [Required]
    [MaxLength(12)]
    public string PostalCode { get; set; }
    [Required]
    [MaxLength(255)]
    public string City { get; set; }
    [Required]
    public CurrencyCode CurrencyCode { get; set; }
    [Required]
    public CountryCode CountryCode { get; set; }
}