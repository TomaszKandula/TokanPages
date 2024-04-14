using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models.Sections;

[ExcludeFromCodeCoverage]
public class BankAccount
{
    public string? Number { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? Street { get; set; }

    public string? Address { get; set; }
}
