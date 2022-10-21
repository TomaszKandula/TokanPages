using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Models;

public class ContentDictionary : IPayloadContent
{
    public IDictionary<string, string> Payload { get; set; } 
}