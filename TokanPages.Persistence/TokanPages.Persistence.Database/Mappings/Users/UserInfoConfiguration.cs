using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.Property(info => info.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(info => info.User)
            .WithMany(user => user.UserInformation)
            .HasForeignKey(info => info.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}