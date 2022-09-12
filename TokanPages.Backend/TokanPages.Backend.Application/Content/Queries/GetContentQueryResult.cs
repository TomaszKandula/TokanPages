namespace TokanPages.Backend.Application.Content.Queries;

public class GetContentQueryResult
{
    public string? ContentType { get; set; }

    public string? ContentName { get; set; }

    public dynamic? Content { get; set; }
}