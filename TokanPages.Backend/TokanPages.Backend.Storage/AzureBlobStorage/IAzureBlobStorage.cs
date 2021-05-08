using System.IO;
using System.Threading.Tasks;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    public interface IAzureBlobStorage
    {
        Task<StorageByteContent> ReadAllBytes(string ASourceFilePath);

        Task<StorageStreamContent> OpenRead(string ASourceFilePath);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType, long AMaxLength);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType = Constants.ContentTypes.STREAM);
        
        Task<string> GetFileContentType(string ASourceFilePath);
        
        Task<bool> DeleteFile(string ASourceFilePath);    
    }
}
