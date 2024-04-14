using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class PostOrderOutput
{
    public string? OrderId { get; set; }

    public PayMethods? PayMethods { get; set; }

    public Status? Status { get; set; }

    public string? RedirectUri { get; set; }
}