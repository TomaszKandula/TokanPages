namespace TokanPages.Backend.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This class is responsible only for providing constants to all classes/methods etc. across the application.
    /// It can be a partial class if necessary; and if so, then put the module in the root folder and additional 
    /// partials in other project folders.
    /// </summary>    
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        /// <summary>
        /// Generic cookie names to be used for processing HTTP responses/requests. 
        /// </summary>
        public static class CookieNames
        {
            public const string WEB_TOKEN = "WebToken";
            public const string REFRESH_TOKEN = "RefreshToken";
        }
        
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
            public const string TEXT_PLAIN = "text/plain";
            public const string TEXT_CSV = "text/csv";
            public const string TEXT_HTML = "text/html";
            public const string IMAGE_JPEG = "image/jpeg";
            public const string IMAGE_PNG = "image/png";
            public const string IMAGE_SVG = "image/svg+xml";
            public const string AUDIO_MPEG = "audio/mpeg";
            public const string VIDEO_MPEG = "video/mpeg";
        }

        public static class MetricNames
        {
            public const string BUGS = "bugs";
            public const string CODE_SMELLS = "code_smells";
            public const string COVERAGE = "coverage";
            public const string DUPLICATED_LINES_DENSITY = "duplicated_lines_density";
            public const string NCLOC = "ncloc";
            public const string SQALE_RATING = "sqale_rating";
            public const string ALERT_STATUS = "alert_status";
            public const string RELIABILITY_RATING = "reliability_rating";
            public const string SECURITY_RATING = "security_rating";
            public const string SQALE_INDEX = "sqale_index";
            public const string VULNERABILITIES = "vulnerabilities";

            public static List<string> NameList { get; } = new()
            {
                BUGS,
                CODE_SMELLS,
                COVERAGE,
                DUPLICATED_LINES_DENSITY,
                NCLOC,
                SQALE_RATING,
                ALERT_STATUS,
                RELIABILITY_RATING,
                SECURITY_RATING,
                SQALE_INDEX,
                VULNERABILITIES
            };
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