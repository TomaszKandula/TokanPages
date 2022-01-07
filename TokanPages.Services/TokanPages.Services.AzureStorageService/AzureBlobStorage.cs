#nullable enable
namespace TokanPages.Services.AzureStorageService;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Models;
using Backend.Shared;
using Backend.Core.Exceptions;
using Backend.Core.Extensions;
using Backend.Shared.Resources;

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

    public virtual async Task<StorageStreamContent?> OpenRead(string sourceFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

        var blobClient = _container.GetBlobClient(sourceFilePath);
        var stream = await blobClient.OpenReadAsync(cancellationToken: cancellationToken);
        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

        return new StorageStreamContent
        {
            Content = stream,
            ContentType = properties.Value.ContentType
        };
    }

    public async Task<List<string>> GetBlobListing(string? filterByPath = default, int pageSize = 10, string? continuationToken = default,
        CancellationToken cancellationToken = default)
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

    public virtual async Task UploadFile(Stream sourceStream, string destinationPath, string contentType = Constants.ContentTypes.Stream, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(destinationPath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(destinationPath)}' cannot be null or empty.");

        var blobClient = _container.GetBlobClient(destinationPath);
        var blobHttpHeaders = new BlobHttpHeaders { ContentType = contentType };
        await blobClient.UploadAsync(sourceStream, blobHttpHeaders, cancellationToken: cancellationToken);
    }

    public async Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

        var blobClient = _container.GetBlobClient(sourceFilePath);
        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

        return properties.Value.ContentType;
    }

    public virtual async Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");
            
        return await _container
            .GetBlobClient(sourceFilePath)
            .DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken = default)
    {
        VerifyUploadContentArguments(content, destinationPath);
            
        var toUpload = content;
        if (!content.IsBase64String())
            toUpload = content.ToBase64Encode();

        var bytes = Convert.FromBase64String(toUpload);
        var contents = new MemoryStream(bytes);

        try
        {
            await UploadFile(contents, destinationPath, cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), exception.Message);
        }
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