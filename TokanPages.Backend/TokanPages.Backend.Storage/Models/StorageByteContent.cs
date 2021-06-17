using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Storage.Models
{
    [ExcludeFromCodeCoverage]
    public class StorageByteContent
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}