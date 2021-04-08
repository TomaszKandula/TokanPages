namespace TokanPages.Backend.Shared
{
    /// <summary>
    /// This class is responsible only for providing constants to all classes/methods etc. accross the application.
    /// It can be a partial class if necessary; and if so, then put the module in the root folder and additional 
    /// partials in other project folders.
    /// </summary>    
    public static class Constants
    {
        public static class Likes 
        {
            public const int LIKES_LIMIT_FOR_ANONYMOUS = 25;
            public const int LIKES_LIMIT_FOR_USER = 50;
        }
        
        public static class Emails 
        {
            public static class Templates
            {
                public const string NEWSLETTER = "/content/templates/newsletter.html";
                public const string CONTACT_FORM = "/content/templates/contactform.html";
            }

            public static class Addresses 
            {
                public const string CONTACT = "contact@tomkandula.com";
                public const string PERSONAL = "tom@tomkandula.com";
                public const string ADMIN = "admin@tomkandula.com";
                public const string PRIVATE = "tomasz.kandula@gmail.com";
            }
        }
    }
}
