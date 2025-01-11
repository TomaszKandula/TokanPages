using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserNoteConfiguration : IEntityTypeConfiguration<UserNote>
{
    public void Configure(EntityTypeBuilder<UserNote> builder)
    {
        builder.Property(userNote => userNote.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(userNote => userNote.Users)
            .WithMany(users => users.UserNote)
            .HasForeignKey(userNote => userNote.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserNote_Users");
    }
}