namespace TokanPages.Backend.Application.Handlers.Queries.Content;

public class GetContentQueryResult
{
    public string? ContentType { get; set; }

    public string? ContentName { get; set; }

    public dynamic? Content { get; set; }
}