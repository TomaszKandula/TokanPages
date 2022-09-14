using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.Property(userInfo => userInfo.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(userInfo => userInfo.UserNavigation)
            .WithMany(users => users.UserInfoNavigation)
            .HasForeignKey(userInfo => userInfo.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserInfo_Users");
    }
}