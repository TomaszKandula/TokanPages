namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Authentication
{
    public class BearerAuthentication : IAuthentication
    {
        public string Token { get; set; }
    }
}