namespace TokanPages.Backend.Shared.Dto.Users
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ResetUserPasswordDto
    {
        public string EmailAddress { get; set; }        
    }
}