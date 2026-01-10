using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Services.VatService.Models;

[ExcludeFromCodeCoverage]
public class VatValidationRequest
{
    public string VatNumber { get; set; }

    public IEnumerable<VatNumberPattern> VatNumberPatterns { get; set; }

    public PolishVatNumberOptions Options { get; set; }

    public VatValidationRequest(string vatNumber, IEnumerable<VatNumberPattern> vatNumberPatterns, PolishVatNumberOptions options)
    {
        VatNumber = vatNumber;
        VatNumberPatterns = vatNumberPatterns;
        Options = options;
    }
}