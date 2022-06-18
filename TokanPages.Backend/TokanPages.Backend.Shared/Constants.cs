namespace TokanPages.Backend.Shared;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Constants
{
    /// <summary>
    /// Selected list of common MIME content types.
    /// </summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types"/>
    public static class ContentTypes
    {
        public const string Stream = "application/octet-stream";
        public const string Zip = "application/zip";
        public const string Json = "application/json";
        public const string Pdf = "application/pdf";
        public const string TextPlain = "text/plain";
        public const string TextCsv = "text/csv";
        public const string TextHtml = "text/html";
        public const string ImageJpeg = "image/jpeg";
        public const string ImagePng = "image/png";
        public const string ImageSvg = "image/svg+xml";
        public const string AudioMpeg = "audio/mpeg";
        public const string VideoMpeg = "video/mpeg";
    }
}