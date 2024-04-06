using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.VatService.Models;

[ExcludeFromCodeCoverage]
public class PolishVatNumberOptions
{
    public bool CalculateCheckSum { get; set; }

    public bool CheckZeros { get; set; }

    public PolishVatNumberOptions(bool checkZeros, bool calculateCheckSum)
    {
        CheckZeros = checkZeros;
        CalculateCheckSum = calculateCheckSum;
    }
}