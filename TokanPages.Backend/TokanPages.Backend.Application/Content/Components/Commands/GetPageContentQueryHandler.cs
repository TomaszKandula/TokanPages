using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class GetPageContentQueryHandler : RequestHandler<GetPageContentQuery, GetPageContentQueryResult>
{
    private const string DefaultLanguage = "eng";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    public GetPageContentQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IJsonSerializer jsonSerializer) 
        : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<GetPageContentQueryResult> Handle(GetPageContentQuery request, CancellationToken cancellationToken)
    {
        var count = request.Components.Count;
        var selectedLanguage = string.IsNullOrEmpty(request.Language) 
            ? DefaultLanguage
            : request.Language;

        ParallelOptions parallelOptions = new() { MaxDegreeOfParallelism = count > 20 ? 20 : count };

        var range = Enumerable.Range(0, count);
        var content = new List<GetContentQueryResult>(count);

        await Parallel.ForEachAsync(range, parallelOptions, async (index, _) =>
        {
            var result = await RequestComponent(request.Components[index], selectedLanguage, cancellationToken);
            content.Add(result);
        });

        return new GetPageContentQueryResult
        {
            Components = content,
            Language = selectedLanguage
        };
    }

    private async Task<GetContentQueryResult> RequestComponent(ContentModel content, string? language, CancellationToken cancellationToken)
    {
        var query = new GetContentQuery
        {
            ContentType = content.ContentType,
            ContentName = content.ContentName,
            Language = language
        };

        var handler = new GetContentQueryHandler(
            DatabaseContext, 
            LoggerService, 
            _jsonSerializer, 
            _azureBlobStorageFactory);

        return await handler.Handle(query, cancellationToken);
    }
}