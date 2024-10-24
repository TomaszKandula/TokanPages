namespace TokanPages.Services.SpaCachingService.Models;

public class UploadFileOutputDto
{
    public string UploadedFileSize { get; set; } = "";

    public string CurrentDirectorySize { get; set; } = "";

    public string FreeSpace { get; set; } = "";

    public string InternalDirectorySizeLimit { get; set; } = "";
}