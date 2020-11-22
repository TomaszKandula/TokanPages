using System.Threading.Tasks;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Storage
{

    public interface IAzureStorageService
    {
        string GetBaseUrl { get; }
        Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath);
        Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName);
    }

}
