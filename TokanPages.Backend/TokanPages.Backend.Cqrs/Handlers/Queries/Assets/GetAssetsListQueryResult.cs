namespace TokanPages.Backend.Cqrs.Handlers.Queries.Assets;

using System.Collections.Generic;

public class GetAssetsListQueryResult
{
    public IEnumerable<string>? Assets { get; set; } 
}