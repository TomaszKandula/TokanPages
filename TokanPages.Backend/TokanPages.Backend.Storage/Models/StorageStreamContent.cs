namespace TokanPages.Backend.Storage.Models
{
    using System.IO;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class StorageStreamContent
    {
        public Stream Content { get; set; }

        public string ContentType { get; set; }
    }
}