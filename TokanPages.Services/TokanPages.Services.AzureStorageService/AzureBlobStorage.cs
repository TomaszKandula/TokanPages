using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Constants;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.AzureStorageService.Models;

namespace TokanPages.Services.AzureStorageService;

public class AzureBlobStorage : IAzureBlobStorage
{
    private readonly BlobContainerClient _container;

    public AzureBlobStorage(BlobContainerClient container) => _container = container;

    public AzureBlobStorage(string connectionString, string containerName)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        _container = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public virtual async Task<StorageByteContent?> ReadAllBytes(string sourceFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

        try
        {
            var blobClient = _container.GetBlobClient(sourceFilePath);

            if (!await blobClient.ExistsAsync(cancellationToken))
                return null;

            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            var contentType = properties.Value.ContentType;

            var response = await blobClient.DownloadContentAsync(cancellationToken);
            var content = response.Value.Content;

            return new StorageByteContent
            {
                Content = content,
                ContentType = contentType
            };
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE), exception.Message);
        }
    }

    public virtual async Task<StorageStreamContent?> OpenRead(string sourceFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

        try
        {
            var blobClient = _container.GetBlobClient(sourceFilePath);
            var stream = await blobClient.OpenReadAsync(cancellationToken: cancellationToken);
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

            return new StorageStreamContent
            {
                Content = stream,
                ContentType = properties.Value.ContentType
            };
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE), exception.Message);
        }
    }

    public async Task<List<string>> GetBlobListing(string? filterByPath = default, int pageSize = 10, string? continuationToken = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var blobPages = _container
                .GetBlobsAsync(cancellationToken: cancellationToken)
                .AsPages(continuationToken, pageSize);

            var result = new List<string>();
            await foreach (Azure.Page<BlobItem> blobPage in blobPages.WithCancellation(cancellationToken))
            {
                var pageItems = blobPage.Values.Select(blobItem => blobItem.Name);
                var shouldFilter = !string.IsNullOrEmpty(filterByPath);
            
                result.AddRange(pageItems.WhereIf(shouldFilter, item => item.Contains(filterByPath!)));
            }

            return result;
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE), exception.Message);
        }
    }

    public async Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

        try
        {
            var blobClient = _container.GetBlobClient(sourceFilePath);
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

            return properties.Value.ContentType;
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE), exception.Message);
        }
    }

    public virtual async Task UploadFile(Stream sourceStream, string destinationPath, string contentType = ContentTypes.Stream, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(destinationPath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(destinationPath)}' cannot be null or empty.");

        try
        {
            var blobClient = _container.GetBlobClient(destinationPath);
            var blobHttpHeaders = new BlobHttpHeaders { ContentType = contentType };
            await blobClient.UploadAsync(sourceStream, blobHttpHeaders, cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), exception.Message);
        }
    }

    public virtual async Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken = default)
    {
        try
        {
            VerifyUploadContentArguments(content, destinationPath);
            
            var toUpload = content;
            if (!content.IsBase64String())
                toUpload = content.ToBase64Encode();

            var bytes = Convert.FromBase64String(toUpload);
            var contents = new MemoryStream(bytes);

            await UploadFile(contents, destinationPath, cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), exception.Message);
        }
    }

    public virtual async Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");
            
        return await _container
            .GetBlobClient(sourceFilePath)
            .DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    private static void VerifyUploadContentArguments(string content, string destinationPath)
    {
        if (string.IsNullOrEmpty(content))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(content)}' cannot be null or empty.");
            
        if (string.IsNullOrEmpty(destinationPath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(destinationPath)}' cannot be null or empty.");
    }
}