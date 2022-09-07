using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.UserInfo;

namespace TokanPages.Persistence.Database.Initializer.Seeders;

[ExcludeFromCodeCoverage]
public static class UserInfoSeeder
{
    public static IEnumerable<UserInfo> SeedUserInfo()
    {
        return new List<UserInfo>
        {
            new()
            {
                Id = UserInfo1.Id,
                UserId = UserInfo1.UserId,
                FirstName = UserInfo1.FirstName,
                LastName = UserInfo1.LastName,
                UserAboutText = UserInfo1.UserAboutText,
                UserImageName = UserInfo1.UserImageName,
                UserVideoName = UserInfo1.UserVideoName,
                CreatedBy = UserInfo1.CreatedBy,
                CreatedAt = UserInfo1.CreatedAt,
                ModifiedBy = UserInfo1.ModifiedBy,
                ModifiedAt = UserInfo1.ModifiedAt
            },
            new()
            {
                Id = UserInfo2.Id,
                UserId = UserInfo2.UserId,
                FirstName = UserInfo2.FirstName,
                LastName = UserInfo2.LastName,
                UserAboutText = UserInfo2.UserAboutText,
                UserImageName = UserInfo2.UserImageName,
                UserVideoName = UserInfo2.UserVideoName,
                CreatedBy = UserInfo2.CreatedBy,
                CreatedAt = UserInfo2.CreatedAt,
                ModifiedBy = UserInfo2.ModifiedBy,
                ModifiedAt = UserInfo2.ModifiedAt
            },
            new()
            {
                Id = UserInfo3.Id,
                UserId = UserInfo3.UserId,
                FirstName = UserInfo3.FirstName,
                LastName = UserInfo3.LastName,
                UserAboutText = UserInfo3.UserAboutText,
                UserImageName = UserInfo3.UserImageName,
                UserVideoName = UserInfo3.UserVideoName,
                CreatedBy = UserInfo3.CreatedBy,
                CreatedAt = UserInfo3.CreatedAt,
                ModifiedBy = UserInfo3.ModifiedBy,
                ModifiedAt = UserInfo3.ModifiedAt
            }
        };
    }
}