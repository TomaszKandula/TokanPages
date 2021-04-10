using System.IO;

namespace TokanPages.Backend.Storage.Models
{
    public class StorageStreamContent
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}