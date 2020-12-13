namespace TokanPages.Backend.Shared.Dto.Responses
{

    public class VerifyEmailAddressResponse
    {
        public bool IsFormatCorrect { get; set; }
        public bool IsDomainCorrect { get; set; }
    }

}
