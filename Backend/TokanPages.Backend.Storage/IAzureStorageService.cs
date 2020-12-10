using System.Threading.Tasks;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage
{

    public interface IAzureStorageService
    {
        string GetBaseUrl { get; }
        Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath);
        Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName);
    }

}
