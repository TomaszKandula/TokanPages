using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class RequestPageDataCommandHandler : RequestHandler<RequestPageDataCommand, RequestPageDataCommandResult>
{
    private const string DefaultLanguage = "eng";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    public RequestPageDataCommandHandler(ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory, 
        IJsonSerializer jsonSerializer) : base(loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<RequestPageDataCommandResult> Handle(RequestPageDataCommand request, CancellationToken cancellationToken)
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

        return new RequestPageDataCommandResult
        {
            Components = content,
            PageName = request.PageName,
            Language = selectedLanguage
        };
    }

    private async Task<GetContentQueryResult> RequestComponent(ContentModel content, string? language, CancellationToken cancellationToken)
    {
        var query = new GetContentQuery
        {
            ContentName = content.ContentName,
            Language = language
        };

        var handler = new GetContentQueryHandler(
            LoggerService, 
            _jsonSerializer, 
            _azureBlobStorageFactory);

        return await handler.Handle(query, cancellationToken);
    }
}