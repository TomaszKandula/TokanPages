using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Data.UserInfo;

[ExcludeFromCodeCoverage]
public static class UserInfo1
{
    public static readonly Guid Id = Guid.Parse("eb067300-8e09-4096-8d6b-bfcc6f370d24");

    public static readonly Guid UserId = User1.Id;

    public const string FirstName = "Ester";

    public const string LastName = "Exposito";

    public const string UserAboutText = "Spanish Software Developer";

    public const string UserImageName = "";

    public const string UserVideoName = "";
       
    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-10 12:15:15");
    
    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;
}