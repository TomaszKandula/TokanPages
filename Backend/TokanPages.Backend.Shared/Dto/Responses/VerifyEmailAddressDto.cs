namespace TokanPages.Backend.Shared.Dto.Responses
{

    public class VerifyEmailAddressDto
    {
        public bool IsFormatCorrect { get; set; }
        public bool IsDomainCorrect { get; set; }
    }

}
