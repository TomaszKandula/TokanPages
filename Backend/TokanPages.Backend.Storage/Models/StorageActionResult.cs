namespace TokanPages.Backend.Storage.Models
{
    public class StorageActionResult
    {
        public bool IsSucceeded { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; } = "n/a";
    }
}
