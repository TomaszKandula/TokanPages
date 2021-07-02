using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Identity.Authorization
{
    [ExcludeFromCodeCoverage]
    public static class Permissions
    {
        public static string CanSelectArticles => nameof(CanSelectArticles);

        public static string CanInsertArticles => nameof(CanInsertArticles);

        public static string CanUpdateArticles => nameof(CanUpdateArticles);

        public static string CanPublishArticles => nameof(CanPublishArticles);

        public static string CanAddLikes => nameof(CanAddLikes);

        public static string CanSelectComments => nameof(CanSelectComments);

        public static string CanInsertComments => nameof(CanInsertComments);

        public static string CanUpdateComments => nameof(CanUpdateComments);

        public static string CanPublishComments => nameof(CanPublishComments);

        public static string CanSelectPhotos => nameof(CanSelectPhotos);

        public static string CanInsertPhotos => nameof(CanInsertPhotos);

        public static string CanUpdatePhotos => nameof(CanUpdatePhotos);

        public static string CanPublishPhotos => nameof(CanPublishPhotos);

        public static string CanSelectPhotoAlbums => nameof(CanSelectPhotoAlbums);

        public static string CanInsertPhotoAlbums => nameof(CanInsertPhotoAlbums);

        public static string CanUpdatePhotoAlbums => nameof(CanUpdatePhotoAlbums);

        public static string CanPublishPhotoAlbums => nameof(CanPublishPhotoAlbums);
    }
}