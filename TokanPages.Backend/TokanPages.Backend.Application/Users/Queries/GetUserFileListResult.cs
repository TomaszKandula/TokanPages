namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListResult
{
    public List<string> FileBlobs { get; set; } = new();
}