using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class PreparePostOrderOutput
{
    public string? OrderUrl { get; set; }

    public ContentString? Content { get; set; }

    public BearerAuthentication? Authentication { get; set; }
}