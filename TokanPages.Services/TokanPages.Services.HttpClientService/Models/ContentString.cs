using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class ContentString : IPayloadContent
{
    public object? Payload { get; set; }
}