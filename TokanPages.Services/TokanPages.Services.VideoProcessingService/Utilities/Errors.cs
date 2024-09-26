using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.VideoProcessingService.Utilities;

[ExcludeFromCodeCoverage]
public static class Errors
{
    public  const string ErrorNoVideo = "Cannot find new video for given ticked ID.";

    public  const string ErrorNoStreamContent = "Stream content is empty.";
}