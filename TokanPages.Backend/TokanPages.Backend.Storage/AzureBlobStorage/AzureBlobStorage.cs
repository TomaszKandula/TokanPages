namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Models;
    using Shared;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;

    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly BlobContainerClient _container;

        public AzureBlobStorage(string connectionString, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            _container = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public virtual async Task<StorageByteContent> ReadAllBytes(string sourceFilePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new AggregateException($"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

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

        public virtual async Task<StorageStreamContent> OpenRead(string sourceFilePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new AggregateException($"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

            var blobClient = _container.GetBlobClient(sourceFilePath);
            var stream = await blobClient.OpenReadAsync(cancellationToken: cancellationToken);
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

            return new StorageStreamContent
            {
                Content = stream,
                ContentType = properties.Value.ContentType
            };
        }

        public virtual async Task UploadFile(Stream sourceStream, string destinationPath, CancellationToken cancellationToken, string contentType = Constants.ContentTypes.Stream)
        {
            if (string.IsNullOrEmpty(destinationPath))
                throw new AggregateException($"Argument '{nameof(destinationPath)}' cannot be null or empty.");

            var blobClient = _container.GetBlobClient(destinationPath);
            var blobHttpHeaders = new BlobHttpHeaders { ContentType = contentType };
            await blobClient.UploadAsync(sourceStream, blobHttpHeaders, cancellationToken: cancellationToken);
        }

        public async Task<string> GetFileContentType(string sourceFilePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new AggregateException($"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");

            var blobClient = _container.GetBlobClient(sourceFilePath);
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

            return properties.Value.ContentType;
        }

        public virtual async Task<bool> DeleteFile(string sourceFilePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new AggregateException($"Argument '{nameof(sourceFilePath)}' cannot be null or empty.");
            
            return await _container
                .GetBlobClient(sourceFilePath)
                .DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task UploadContent(string content, string destinationPath, CancellationToken cancellationToken)
        {
            VerifyUploadContentArguments(content, destinationPath);
            
            var toUpload = content;
            if (!content.IsBase64String())
                toUpload = content.ToBase64Encode();

            var bytes = Convert.FromBase64String(toUpload);
            var contents = new MemoryStream(bytes);

            try
            {
                await UploadFile(contents, destinationPath, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), exception.Message);
            }
        }

        private static void VerifyUploadContentArguments(string content, string destinationPath)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException($"Argument '{nameof(content)}' cannot be null or empty.");
            
            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentException($"Argument '{nameof(destinationPath)}' cannot be null or empty.");
        }
    }
}