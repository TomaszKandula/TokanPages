namespace TokanPages.BackEnd.Storage.Model
{

    public class ActionResult
    {
        public bool IsSucceeded { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; } = "n/a";
    }

}
