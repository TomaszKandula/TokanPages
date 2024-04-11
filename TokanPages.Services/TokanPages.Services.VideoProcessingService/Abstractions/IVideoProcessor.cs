using TokanPages.Services.VideoProcessingService.Models;

namespace TokanPages.Services.VideoProcessingService.Abstractions;

public interface IVideoProcessor
{
    Task Process(RequestVideoProcessing request);
}