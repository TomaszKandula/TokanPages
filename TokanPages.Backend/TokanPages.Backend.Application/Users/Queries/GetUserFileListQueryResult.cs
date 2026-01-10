namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQueryResult
{
    public List<string> FileBlobs { get; set; } = new();
}