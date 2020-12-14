using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.Models;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public class TemplateHelper : ITemplateHelper
    {

        public async Task<string> MakeBody(string ATemplate, List<ValueTag> AValueTag, string ATemplateSource)
        {

            var LStorageUrl = $"{ATemplateSource}{ATemplate}";
            var LTemplate = await GetFileFromUrl(LStorageUrl);

            if (AValueTag == null || !AValueTag.Any()) return null;

            foreach (var AItem in AValueTag)
            {
                LTemplate = LTemplate.Replace(AItem.Tag, AItem.Value);
            }

            return LTemplate;

        }

        private async Task<string> GetFileFromUrl(string Url)
        {
            try
            {
                var LHttpClient = new HttpClient();
                var LResponse = await LHttpClient.GetAsync(Url);
                return await LResponse.Content.ReadAsStringAsync();
            }
            catch
            {
                return Url;
            }
        }

    }

}
