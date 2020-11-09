namespace TokanPages.BackEnd.Mailer.Model
{

    public class Result
    {
        public bool IsSucceeded { get; set; }
        public string ErrorMessage { get; set; } = "n/a";
        public int ErrorCode { get; set; }
    }

}
