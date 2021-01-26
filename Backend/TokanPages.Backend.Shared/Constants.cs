namespace TokanPages.Backend.Shared
{
    /// <summary>
    /// This class is responsible only for providing constants to all classes/methods etc. accross the application.
    /// It can be a partial class if necessary; and if so, then put the module in the root folder and additional 
    /// partials in other project folders.
    /// </summary>    
    public static class Constants
    {
        public static class Emails 
        {
            public static class Templates
            {
                public const string Newsletter = "/content/templates/newsletter.html";
                public const string ContactForm = "/content/templates/contactform.html";
            }

            public static class Addresses 
            {
                public const string Contact = "contact@tomkandula.com";
                public const string Personal = "tom@tomkandula.com";
                public const string Admin = "admin@tomkandula.com";
                public const string Private = "tomasz.kandula@gmail.com";
            }
        }
    }
}
