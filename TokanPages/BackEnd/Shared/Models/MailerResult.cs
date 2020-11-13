namespace TokanPages.BackEnd.Shared.Models
{

    public class MailerResult
    {
        public bool IsSucceeded { get; set; }
        public string ErrorMessage { get; set; } = "n/a";
        public string ErrorCode { get; set; }
    }

}
