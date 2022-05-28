namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserTokensConfiguration : IEntityTypeConfiguration<UserTokens>
{
    public void Configure(EntityTypeBuilder<UserTokens> builder)
    {
        builder.Property(userTokens => userTokens.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userTokens => userTokens.UserNavigation)
            .WithMany(users => users.UserTokensNavigation)
            .HasForeignKey(userTokens => userTokens.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserTokens_Users");
    }
}