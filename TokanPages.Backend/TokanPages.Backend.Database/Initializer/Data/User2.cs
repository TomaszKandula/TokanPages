using System;

namespace TokanPages.Backend.Database.Initializer.Data
{
    public static class User2
    {
        public const string EMAIL_ADDRESS = "tokan@dfds.com";

        public const string USER_ALIAS = "tokan";
        
        public const string FIRST_NAME = "Tom";
        
        public const string LAST_NAME = "Tom";
        
        public const bool IS_ACTIVATED = true;
        
        public const string AVATAR_NAME = null;
        
        public const string SHORT_BIO = "Software Developer";

        public static Guid FId = Guid.Parse("d6365db3-d464-4146-857b-d8476f46553c");
        
        public static DateTime FRegistered = DateTime.Parse("2020-01-25 05:09:19");
        
        public static DateTime? FLastLogged = DateTime.Parse("2020-03-22 12:00:15");
        
        public static DateTime? FLastUpdated = DateTime.Parse("2020-05-21 05:09:11");
    }
}
