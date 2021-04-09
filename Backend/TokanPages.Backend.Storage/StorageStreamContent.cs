using System.IO;

namespace TokanPages.Backend.Storage
{
    public class StorageStreamContent
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}