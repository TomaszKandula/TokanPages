using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Shared.Constants;
using TokanPages.Services.AzureStorageService.Models;

namespace TokanPages.Services.AzureStorageService;

public interface IAzureBlobStorage
{
    Task<StorageByteContent?> ReadAllBytes(string sourceFilePath, CancellationToken cancellationToken = default);

    Task<StorageStreamContent?> OpenRead(string sourceFilePath, CancellationToken cancellationToken = default);

    Task<List<string>> GetBlobListing(string? filterByPath = default, int pageSize = 10, string? continuationToken = default, CancellationToken cancellationToken = default);

    Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken = default);

    Task UploadFile(Stream sourceStream, string destinationPath, string contentType = ContentTypes.Stream, CancellationToken cancellationToken = default);

    Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken = default);

    Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken = default);
}