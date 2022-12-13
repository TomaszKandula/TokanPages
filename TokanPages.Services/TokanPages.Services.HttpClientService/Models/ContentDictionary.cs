using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class ContentDictionary : IPayloadContent
{
    public IDictionary<string, string> Payload { get; set; } 
}