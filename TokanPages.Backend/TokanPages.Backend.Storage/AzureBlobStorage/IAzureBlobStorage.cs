using System;
using System.IO;
using System.Threading.Tasks;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    public interface IAzureBlobStorage
    {
        Task<StorageByteContentModel> ReadAllBytes(string ASourceFilePath);

        Task<StorageStreamContentModel> OpenRead(string ASourceFilePath);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType, long AMaxLength);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType = Constants.ContentTypes.STREAM);
        
        Task<string> GetFileContentType(string ASourceFilePath);
        
        Task<bool> DeleteFile(string ASourceFilePath);

        Task UploadText(Guid AId, string ATextToUpload);

        Task UploadImage(Guid AId, string AImageToUpload);
    }
}
