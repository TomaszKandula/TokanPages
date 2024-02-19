using TokanPages.Services.VideoConverterService.Models;

namespace TokanPages.Services.VideoConverterService.Abstractions;

public interface IVideoConverter
{
    string WorkingDir { get; }

    Task<ConverterOutput> Convert(byte[] sourceData, string sourceFileName, bool shouldCompactVideo, CancellationToken cancellationToken = default);

    void GetVideoThumbnail(string localInput, string localOutput);
}