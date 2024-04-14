using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class OrderTransactionsOutput
{
    public IEnumerable<Transactions>? Transactions { get; set; }
}