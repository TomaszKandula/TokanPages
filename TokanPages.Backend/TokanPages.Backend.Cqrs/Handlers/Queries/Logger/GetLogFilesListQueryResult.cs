namespace TokanPages.Backend.Cqrs.Handlers.Queries.Logger;

using System.Collections.Generic;

public class GetLogFilesListQueryResult
{
    public List<string> LogFiles { get; set; }
}