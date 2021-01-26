namespace TokanPages.Backend.SmtpClient.Settings
{
    public class SmtpServerSettings
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public bool IsSSL { get; set; }
    }
}
