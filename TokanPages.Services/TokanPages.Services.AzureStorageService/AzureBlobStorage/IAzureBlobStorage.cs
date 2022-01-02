namespace TokanPages.Services.AzureStorageService.AzureBlobStorage;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Models;
using Backend.Shared;

public interface IAzureBlobStorage
{
    Task<StorageByteContent> ReadAllBytes(string sourceFilePath ,CancellationToken cancellationToken);

    Task<StorageStreamContent> OpenRead(string sourceFilePath, CancellationToken cancellationToken);
        
    Task UploadFile(Stream sourceStream, string destinationPath, CancellationToken cancellationToken, string contentType = Constants.ContentTypes.Stream);
        
    Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken);
        
    Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken);

    Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken);
}