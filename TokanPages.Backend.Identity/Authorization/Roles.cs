using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Identity.Authorization
{
    [ExcludeFromCodeCoverage]
    public static class Roles
    {
        public static string GodOfAsgard => nameof(GodOfAsgard);
        
        public static string EverydayUser => nameof(EverydayUser);
        
        public static string ArticlePublisher => nameof(ArticlePublisher);
        
        public static string PhotoPublisher => nameof(PhotoPublisher);
        
        public static string CommentPublisher => nameof(CommentPublisher);
    }
}