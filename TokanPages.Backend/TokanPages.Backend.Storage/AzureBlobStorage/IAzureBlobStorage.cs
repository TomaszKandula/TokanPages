namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Shared;

    public interface IAzureBlobStorage
    {
        Task<StorageByteContent> ReadAllBytes(string ASourceFilePath ,CancellationToken ACancellationToken);

        Task<StorageStreamContent> OpenRead(string ASourceFilePath, CancellationToken ACancellationToken);
        
        Task UploadFile(Stream ASourceStream, string ADestinationPath, CancellationToken ACancellationToken, string AContentType = Constants.ContentTypes.STREAM);
        
        Task<string> GetFileContentType(string ASourceFilePath, CancellationToken ACancellationToken);
        
        Task<bool> DeleteFile(string ASourceFilePath, CancellationToken ACancellationToken);

        Task UploadContent(string AContent, string ADestinationPath, CancellationToken ACancellationToken);
    }
}