namespace TokanPages.Backend.Shared
{
    /// <summary>
    /// This class is responsible only for providing constants to all classes/methods etc. across the application.
    /// It can be a partial class if necessary; and if so, then put the module in the root folder and additional 
    /// partials in other project folders.
    /// </summary>    
    public static class Constants
    {
        /// <summary>
        /// Selected list of common MIME content types.
        /// </summary>
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types"/>
        public static class ContentTypes
        {
            public const string STREAM = "application/octet-stream";
            public const string ZIP = "application/zip";
            public const string JSON = "application/json";
            public const string PDF = "application/pdf";
            public const string TEXT_CSV = "text/csv";
            public const string TEXT_HTML = "text/html";
            public const string IMAGE_JPEG = "image/jpeg";
            public const string IMAGE_PNG = "image/png";
            public const string IMAGE_SVG = "image/svg+xml";
            public const string AUDIO_MPEG = "audio/mpeg";
            public const string VIDEO_MPEG = "video/mpeg";
        }

        /// <summary>
        /// Holds limits of likes for logged user and anonymous user.
        /// </summary>
        public static class Likes 
        {
            public const int LIKES_LIMIT_FOR_ANONYMOUS = 25;
            public const int LIKES_LIMIT_FOR_USER = 50;
        }

        /// <summary>
        /// Basic email constants related to the portal needs only.
        /// </summary>
        public static class Emails 
        {
            /// <summary>
            /// Selection of various templates.
            /// </summary>
            public static class Templates
            {
                public const string NEWSLETTER = "/content/templates/newsletter.html";
                public const string CONTACT_FORM = "/content/templates/contactform.html";
            }

            /// <summary>
            /// Basic email addresses for email communication.
            /// </summary>
            public static class Addresses 
            {
                public const string ADMIN = "admin@tomkandula.com";
                public const string CONTACT = "contact@tomkandula.com";
                public const string IT_SUPPORT = "it.support@tomkandula.com";
            }
        }
    }
}
