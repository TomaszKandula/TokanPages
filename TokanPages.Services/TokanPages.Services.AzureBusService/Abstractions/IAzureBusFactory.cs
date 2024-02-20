namespace TokanPages.Services.AzureBusService.Abstractions;

public interface IAzureBusFactory
{
    IAzureBusClient Create();
}