using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Mailer
{
    [ExcludeFromCodeCoverage]
    public class VerifyEmailAddressDto
    {
        public string Email { get; set; }
    }
}
