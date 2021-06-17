using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Backend.Shared.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientContent
    {
        private const string AUTHORIZATION = "Authorization";

        public static async Task<string> GetContent(string ARequestUrl, string AToken)
        {
            using var LHttpClient = new HttpClient();
            using var LRequest = new HttpRequestMessage(new HttpMethod("GET"), ARequestUrl);

            var LBase64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{AToken}:"));
            LRequest.Headers.TryAddWithoutValidation(AUTHORIZATION, $"Basic {LBase64Authorization}"); 
                
            var LResponse = await LHttpClient.SendAsync(LRequest);
            return await LResponse.Content.ReadAsStringAsync();
        }

        public static ContentResult GetContentResult(int? AStatusCode, string AContent, string AContentType = Constants.ContentTypes.TEXT_PLAIN)
        {
            return new () 
            {
                StatusCode = AStatusCode,
                ContentType = AContentType,
                Content = AContent
            };
        }
        
        public static string GetFirstEmptyParameterName(IEnumerable<ParameterModel> AParameterList)
        {
            foreach (var LParameter in AParameterList
                .Where(AParameter => string.IsNullOrEmpty(AParameter.Value)))
            {
                return LParameter.Key;
            }

            return string.Empty;
        }
    }
}