using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Factory;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetAssetsListQueryHandler : RequestHandler<GetAssetsListQuery, GetAssetsListQueryResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetAssetsListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory) 
        : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<GetAssetsListQueryResult> Handle(GetAssetsListQuery request, CancellationToken cancellationToken)
    {
        const string filteredBy = "content/assets/";
        var azureBlob = _azureBlobStorageFactory.Create();
        var listing = await azureBlob.GetBlobListing(filteredBy, 1000, cancellationToken: cancellationToken);

        return new GetAssetsListQueryResult
        {
            Assets = listing
        };
    }
}