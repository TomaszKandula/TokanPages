using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.TemplateHelper.Model;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public class TemplateHelper : ITemplateHelper
    {

        public async Task<string> MakeBody(string ATemplate, List<Item> AItems, string ATemplateSource)
        {

            var LStorageUrl = $"{ATemplateSource}{ATemplate}";
            var LTemplate = await GetFileFromUrl(LStorageUrl);

            if (AItems == null || !AItems.Any()) return null;

            foreach (var AItem in AItems)
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
