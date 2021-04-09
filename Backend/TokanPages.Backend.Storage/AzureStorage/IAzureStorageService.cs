using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage.AzureStorage
{
    public interface IAzureStorageService
    {
        string GetBaseUrl { get; }
        Task<ActionResult> UploadFile(string ADestContainerReference, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken);
        Task<ActionResult> RemoveFromStorage(string AContainerReference, string AFileName, CancellationToken ACancellationToken);
    }
}
