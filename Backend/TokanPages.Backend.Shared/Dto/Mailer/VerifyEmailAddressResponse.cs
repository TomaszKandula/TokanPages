namespace TokanPages.Backend.Shared.Dto.Mailer
{

    public class VerifyEmailAddressResponse
    {
        public bool IsFormatCorrect { get; set; }
        public bool IsDomainCorrect { get; set; }
    }

}
