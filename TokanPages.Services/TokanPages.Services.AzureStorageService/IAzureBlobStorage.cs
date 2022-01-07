#nullable enable
namespace TokanPages.Services.AzureStorageService;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Backend.Shared;

public interface IAzureBlobStorage
{
    Task<StorageByteContent?> ReadAllBytes(string sourceFilePath, CancellationToken cancellationToken = default);

    Task<StorageStreamContent?> OpenRead(string sourceFilePath, CancellationToken cancellationToken = default);
        
    Task<List<string>> GetBlobListing(string? filterByPath = default, int pageSize = 10, string? continuationToken = default, CancellationToken cancellationToken = default);

    Task UploadFile(Stream sourceStream, string destinationPath, CancellationToken cancellationToken = default, string contentType = Constants.ContentTypes.Stream);
        
    Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken = default);
        
    Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken = default);

    Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken = default);
}