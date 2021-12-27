namespace TokanPages.Backend.Shared;

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
    /// A cost factor for BCrypt algorithm.
    /// </summary>
    /// <see href="https://auth0.com/blog/hashing-in-action-understanding-bcrypt/"/>
    public const int CipherLogRounds = 12;
        
    /// <summary>
    /// Shared default values.
    /// </summary>
    public static class Defaults
    {
        public const string AvatarName = "avatar-default-288.jpeg";
    }

    /// <summary>
    /// Generic cookie names to be used for processing HTTP responses/requests. 
    /// </summary>
    public static class CookieNames
    {
        public const string WebToken = "WebToken";
        public const string RefreshToken = "RefreshToken";
    }
        
    /// <summary>
    /// Selected list of common MIME content types.
    /// </summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types"/>
    public static class ContentTypes
    {
        public const string Stream = "application/octet-stream";
        public const string Zip = "application/zip";
        public const string Json = "application/json";
        public const string Pdf = "application/pdf";
        public const string TextPlain = "text/plain";
        public const string TextCsv = "text/csv";
        public const string TextHtml = "text/html";
        public const string ImageJpeg = "image/jpeg";
        public const string ImagePng = "image/png";
        public const string ImageSvg = "image/svg+xml";
        public const string AudioMpeg = "audio/mpeg";
        public const string VideoMpeg = "video/mpeg";
    }

    /// <summary>
    /// SonarQube available metrics.
    /// </summary>
    public static class MetricNames
    {
        public const string Bugs = "bugs";
        public const string CodeSmells = "code_smells";
        public const string Coverage = "coverage";
        public const string DuplicatedLinesDensity = "duplicated_lines_density";
        public const string Ncloc = "ncloc";
        public const string SqaleRating = "sqale_rating";
        public const string AlertStatus = "alert_status";
        public const string ReliabilityRating = "reliability_rating";
        public const string SecurityRating = "security_rating";
        public const string SqaleIndex = "sqale_index";
        public const string Vulnerabilities = "vulnerabilities";

        public static List<string> NameList { get; } = new()
        {
            Bugs,
            CodeSmells,
            Coverage,
            DuplicatedLinesDensity,
            Ncloc,
            SqaleRating,
            AlertStatus,
            ReliabilityRating,
            SecurityRating,
            SqaleIndex,
            Vulnerabilities
        };
    }

    /// <summary>
    /// Holds limits of likes for logged user and anonymous user.
    /// </summary>
    public static class Likes 
    {
        public const int LikesLimitForAnonymous = 25;
        public const int LikesLimitForUser = 50;
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
            public const string Newsletter = "/content/templates/newsletter.html";
            public const string ContactForm = "/content/templates/contactform.html";
            public const string ResetPassword = "/content/templates/resetpassword.html";
            public const string RegisterForm = "/content/templates/registrationform.html";
        }

        /// <summary>
        /// Basic email addresses for email communication.
        /// </summary>
        public static class Addresses 
        {
            public const string Admin = "admin@tomkandula.com";
            public const string Contact = "contact@tomkandula.com";
            public const string ItSupport = "it.support@tomkandula.com";
        }
    }
}