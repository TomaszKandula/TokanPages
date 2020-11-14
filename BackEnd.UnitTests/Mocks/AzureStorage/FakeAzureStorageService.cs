using System.Threading.Tasks;
using TokanPages.BackEnd.Storage;
using TokanPages.BackEnd.Storage.Model;

namespace BackEnd.UnitTests.Mocks.AzureStorage
{

    public class FakeAzureStorageService : AzureStorageService
    {

        public FakeAzureStorageService() 
        { 
        }

        public new string ReturnBasicUrl { get; }

        public override async Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath)
        {
            return new ActionResult
            {
                IsSucceeded = true
            };
        }

        public override async Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName)
        {
            return new ActionResult
            {
                IsSucceeded = true
            };
        }

    }

}
