using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage
{

    public interface IAzureStorageService
    {
        string GetBaseUrl { get; }
        Task<ActionResult> UploadFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken);
        Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName, CancellationToken ACancellationToken);
    }

}
