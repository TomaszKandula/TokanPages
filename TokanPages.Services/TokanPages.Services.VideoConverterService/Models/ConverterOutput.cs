namespace TokanPages.Services.VideoConverterService.Models;

public class ConverterOutput
{
    public string OutputVideoName { get; set; } = "";

    public string OutputVideoPath { get; set; } = "";

    public string OutputThumbnailName { get; set; } = "";

    public string OutputThumbnailPath { get; set; } = "";

    public long InputSizeInBytes { get; set; }

    public long OutputSizeInBytes { get; set; }

    public string? ProcessingWarning { get; set; }
}