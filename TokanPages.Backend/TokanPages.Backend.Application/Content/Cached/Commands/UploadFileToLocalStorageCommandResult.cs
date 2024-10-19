namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class UploadFileToLocalStorageCommandResult
{
    public string UploadedFileSize { get; set; } = "";

    public string CurrentDirectorySize { get; set; } = "";

    public string FreeSpace { get; set; } = "";

    public string InternalDirectorySizeLimit { get; set; } = "";
}