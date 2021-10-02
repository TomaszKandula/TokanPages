namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Authentication;

    public class CustomHttpClient : ICustomHttpClient
    {
        private const string HEADER = "Authorization";

        private const string BASIC = "Basic";

        private const string BEARER = "Bearer";

        public async Task<(string, HttpStatusCode)> Execute(Configuration AConfiguration, CancellationToken ACancellationToken = default)
        {
            using var LHttpClient = new HttpClient();
            using var LRequest = new HttpRequestMessage(new HttpMethod(AConfiguration.Method), AConfiguration.Url);

            switch (AConfiguration.Authentication)
            {
                case BasicAuthentication LBasicAuthentication:
                    var LBasic = SetAuthentication(LBasicAuthentication.Login, LBasicAuthentication.Password);
                    LRequest.Headers.TryAddWithoutValidation(HEADER, LBasic); 
                    break;
                
                case BearerAuthentication LBearerAuthentication:
                    var LBearer = SetAuthentication(LBearerAuthentication.Token);
                    LRequest.Headers.TryAddWithoutValidation(HEADER, LBearer); 
                    break;
            }

            var LResponse = await LHttpClient.SendAsync(LRequest, ACancellationToken);
            var LContent = await LResponse.Content.ReadAsStringAsync(ACancellationToken);

            return (LContent, LResponse.StatusCode);
        }

        public ContentResult GetContentResult(int? AStatusCode, string AContent, string AContentType)
        {
            return new ContentResult
            {
                StatusCode = AStatusCode,
                ContentType = AContentType,
                Content = AContent
            };
        }

        public string SetAuthentication(string ALogin, string APassword)
        {
            var LBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ALogin}:{APassword}"));
            return $"{BASIC} {LBase64}";
        }

        public string SetAuthentication(string AToken)
        {
            return $"{BEARER} {AToken}";
        }

        public string GetFirstEmptyParameterName(IDictionary<string, string> AParameterList)
        {
            var LParameters = AParameterList
                .Where(AParameter => string.IsNullOrEmpty(AParameter.Value))
                .ToList();

            return LParameters.Any() 
                ? LParameters.Select(AParameterModel => AParameterModel.Key).FirstOrDefault() 
                : string.Empty;
        }
    }
}