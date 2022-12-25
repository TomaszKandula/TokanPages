using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Data.UserInfo;

[ExcludeFromCodeCoverage]
public static class UserInfo2
{
    public static readonly Guid Id = Guid.Parse("f466d050-a767-4b17-b985-9d8e546896a0");

    public static readonly Guid UserId = User2.Id;

    public const string FirstName = "Victoria";

    public const string LastName = "Justice";

    public const string UserAboutText = "American Software Developer";

    public const string UserImageName = "";

    public const string UserVideoName = "";
       
    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-10 12:15:15");
    
    public static readonly Guid? ModifiedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = DateTime.Parse("2020-05-21 05:09:11");
}