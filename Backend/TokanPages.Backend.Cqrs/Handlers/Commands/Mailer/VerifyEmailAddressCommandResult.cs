namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class VerifyEmailAddressCommandResult
    {
        public bool IsFormatCorrect { get; set; }
        public bool IsDomainCorrect { get; set; }
    }
}
