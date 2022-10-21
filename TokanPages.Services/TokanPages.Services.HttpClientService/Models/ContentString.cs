using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Models;

public class ContentString : IPayloadContent
{
    public object? Payload { get; set; }
}