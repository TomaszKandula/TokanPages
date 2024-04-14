using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BaseResponse
{
    public int SystemCode { get; set; }
}