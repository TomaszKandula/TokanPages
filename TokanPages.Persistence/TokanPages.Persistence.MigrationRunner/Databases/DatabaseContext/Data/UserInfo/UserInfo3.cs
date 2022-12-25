using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.UserInfo;

[ExcludeFromCodeCoverage]
public static class UserInfo3
{
    public static readonly Guid Id = Guid.Parse("50f28a8c-9c69-4838-bc33-7854b77bab29");

    public static readonly Guid UserId = User3.Id;

    public const string FirstName = "Jenny";

    public const string LastName = "Marsala";

    public const string UserAboutText = "German Software Developer";

    public const string UserImageName = "";

    public const string UserVideoName = "";
       
    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-12 22:01:33");
    
    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;
}