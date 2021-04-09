using System.IO;
using System.Threading.Tasks;

namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    public interface IAzureBlobStorage
    {
        Task<StorageByteContent> ReadAllBytesAsync(string AFilePath);
        Task<StorageStreamContent> OpenReadAsync(string AFilePath);
        Task UploadFileAsync(Stream AStream, string AFilePath, string AContentType, long AMaxLength);
        Task UploadFileAsync(Stream AStream, string ADestinationFilePath, string AContentType = "application/octet-stream");
        Task<string> GetFileContentType(string AFilePath);
        Task<bool> DeleteFileAsync(string AFilePath);    
    }
}
