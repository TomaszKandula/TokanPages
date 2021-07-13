namespace TokanPages.Backend.Storage.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class StorageByteContent
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}