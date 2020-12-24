using System.Threading;
using System.Threading.Tasks;

namespace TokanPages.Backend.Core.Services.FileUtility
{
    public interface IFileUtility
    {
        Task<string> SaveToFile(string ATemporaryDir, string AFileName, string ATextContent);
        Task<string> GetFileFromUrl(string AUrl, CancellationToken ACancellationToken);
        bool IsBase64String(string ABase64);
    }

}
