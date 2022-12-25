using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Data.UserInfo;

[ExcludeFromCodeCoverage]
public static class UserInfo4
{
    public static readonly Guid Id = Guid.Parse("b9be5533-e667-44c7-8e3a-d21307b4e7df");

    public static readonly Guid UserId = User4.Id;

    public const string FirstName = "Admin";

    public const string LastName = "God";

    public const string UserAboutText = "Admin, God of Asgard";

    public const string UserImageName = "";

    public const string UserVideoName = "";
       
    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-12 22:01:33");
    
    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;
}