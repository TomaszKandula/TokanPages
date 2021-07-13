namespace TokanPages.Backend.Storage.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class StorageByteContentModel
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}