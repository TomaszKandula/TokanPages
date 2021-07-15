namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Models;
    using Shared;

    public interface IAzureBlobStorage
    {
        Task<StorageByteContent> ReadAllBytes(string ASourceFilePath);

        Task<StorageStreamContent> OpenRead(string ASourceFilePath);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType, long AMaxLength);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType = Constants.ContentTypes.STREAM);
        
        Task<string> GetFileContentType(string ASourceFilePath);
        
        Task<bool> DeleteFile(string ASourceFilePath);

        Task UploadText(Guid AId, string ATextToUpload);

        Task UploadImage(Guid AId, string AImageToUpload);
    }
}