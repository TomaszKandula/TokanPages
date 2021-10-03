namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Authentication
{
    public class BasicAuthentication : IAuthentication
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}