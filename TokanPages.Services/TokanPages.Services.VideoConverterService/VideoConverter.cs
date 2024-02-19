using System.Diagnostics;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Services.VideoConverterService.Abstractions;
using TokanPages.Services.VideoConverterService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TokanPages.Services.VideoConverterService;

public class VideoConverter : IVideoConverter
{
    private const string ResourcesDirName = "Resources";

    private const string WorkingDirName = "WorkingDir";

    public string WorkingDir { get; }

    private readonly IJsonSerializer _jsonSerializer;

    public VideoConverter(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var resourceDir = Path.Combine(baseDirectory, ResourcesDirName);
        var workingDir = Path.Combine(resourceDir, WorkingDirName);

        WorkingDir = workingDir;

        if (!Directory.Exists(WorkingDir))
            Directory.CreateDirectory(WorkingDir);
    }

    public async Task<ConverterOutput> Convert(byte[] sourceData, string sourceFileName, 
        bool shouldCompactVideo, CancellationToken cancellationToken = default)
    {
        var fileGuid = $"{Guid.NewGuid():N}";
        var outputFileName = $"{fileGuid}.mp4".ToLower();        
        var thumbnailFileName = $"{fileGuid}.jpg".ToLower();

        var fullInputPath = Path.Combine(WorkingDir, sourceFileName);
        var fullVideoPath = Path.Combine(WorkingDir, outputFileName);
        var fullThumbnailPath = Path.Combine(WorkingDir, thumbnailFileName);

        await File.WriteAllBytesAsync(fullInputPath, sourceData, cancellationToken);
        var processResult = TryProcessVideo(shouldCompactVideo, fullInputPath, fullVideoPath);

        var inputSizeInBytes = new FileInfo(fullInputPath).Length;
        var outputSizeInBytes = new FileInfo(fullVideoPath).Length;

        GetVideoThumbnail(fullVideoPath, fullThumbnailPath);
        File.Delete(fullInputPath);

        return new ConverterOutput
        {
            OutputVideoName = outputFileName,
            OutputVideoPath = fullVideoPath,
            OutputThumbnailName = thumbnailFileName,
            OutputThumbnailPath = fullThumbnailPath,
            ProcessingWarning = processResult,
            InputSizeInBytes = inputSizeInBytes,
            OutputSizeInBytes = outputSizeInBytes
        };
    }

    private string? TryProcessVideo(bool shouldCompactVideo, string localInput, string localOutput)
    {
        try
        {
            if (shouldCompactVideo)
            {
                ProcessRunner(new[]
                {
                    "-i",
                    localInput,
                    "-c:v libx264",
                    "-profile:v baseline",
                    "-crf 25",
                    "-c:a aac",
                    "-r:a 44100",
                    "-b:a 64k",
                    "-movflags faststart",
                    localOutput
                });
            }
            else
            {
                ProcessRunner(new[]
                {
                    "-i",
                    localInput,
                    "-c copy",
                    "-movflags faststart",
                    localOutput
                });
            }
        }
        catch (Exception exception)
        {
            var processingWarning = new ProcessingWarning
            {
                ErrorMessage = exception.Message,
                ErrorInnerMessage = exception.InnerException?.Message,
                Message = $"File has been copied 'as is'; from: [{localInput}] to [{localOutput}]."
            };

            File.Copy(localInput, localOutput);

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var warning = _jsonSerializer.Serialize(processingWarning, Formatting.None, settings);
            return warning;
        }

        return null;
    }

    public void GetVideoThumbnail(string localInput, string localOutput)
    {
        var arguments = new[]
        {
            "-ss 00:00:10",
            "-i",
            localInput,
            "-frames:v 1",
            localOutput
        };

        ProcessRunner(arguments);
    }

    private static void ProcessRunner(string[] arguments)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            WorkingDirectory = "/usr/bin",
            Arguments = string.Join(" ", arguments),
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.Start();
        process.WaitForExit();
    }
}