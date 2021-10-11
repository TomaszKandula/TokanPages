namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Authentication;
    using Models;

    public class CustomHttpClient : ICustomHttpClient
    {
        private const string HEADER = "Authorization";

        private const string BASIC = "Basic";

        private const string BEARER = "Bearer";

        private readonly HttpClient FHttpClient;

        public CustomHttpClient(HttpClient AHttpClient) => FHttpClient = AHttpClient;

        public async Task<Results> Execute(Configuration AConfiguration, CancellationToken ACancellationToken = default)
        {
            if (string.IsNullOrEmpty(AConfiguration.Method))
                throw new ArgumentException($"Argument '{nameof(AConfiguration.Method)}' cannot be null or empty.");

            if (string.IsNullOrEmpty(AConfiguration.Url))
                throw new ArgumentException($"Argument '{nameof(AConfiguration.Url)}' cannot be null or empty.");

            using var LRequest = new HttpRequestMessage(new HttpMethod(AConfiguration.Method), AConfiguration.Url);

            if (AConfiguration.StringContent != null)
                LRequest.Content = AConfiguration.StringContent;
            
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

            var LResponse = await FHttpClient.SendAsync(LRequest, ACancellationToken);
            var LContentType = LResponse.Content.Headers.ContentType;
            var LContent = await LResponse.Content.ReadAsByteArrayAsync(ACancellationToken);

            return new Results
            {
                StatusCode = LResponse.StatusCode,
                ContentType = LContentType,
                Content = LContent
            };
        }

        public string SetAuthentication(string ALogin, string APassword)
        {
            if (string.IsNullOrEmpty(ALogin))
                throw new ArgumentException($"Argument '{nameof(ALogin)}' cannot be null or empty.");

            var LBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ALogin}:{APassword}"));
            return $"{BASIC} {LBase64}";
        }

        public string SetAuthentication(string AToken)
        {
            if (string.IsNullOrEmpty(AToken))
                throw new ArgumentException($"Argument '{nameof(AToken)}' cannot be null or empty.");

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