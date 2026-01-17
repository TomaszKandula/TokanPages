using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(token => token.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(token => token.User)
            .WithMany(user => user.UserTokens)
            .HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}