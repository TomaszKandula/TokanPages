using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TokanPages.Backend.Storage.Models
{
    [ExcludeFromCodeCoverage]
    public class StorageStreamContent
    {
        public Stream Content { get; set; }

        public string ContentType { get; set; }
    }
}