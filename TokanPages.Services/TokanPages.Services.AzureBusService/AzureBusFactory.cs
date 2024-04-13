using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.Services.AzureBusService;

public class AzureBusFactory : IAzureBusFactory
{
    private readonly string _connectionString;

    public AzureBusFactory(string connectionString)
        => _connectionString = connectionString;

    public IAzureBusClient Create() 
        => new AzureBusClient(_connectionString);
}