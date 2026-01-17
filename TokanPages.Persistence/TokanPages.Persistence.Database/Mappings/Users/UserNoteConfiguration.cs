using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserNoteConfiguration : IEntityTypeConfiguration<UserNote>
{
    public void Configure(EntityTypeBuilder<UserNote> builder)
    {
        builder.Property(note => note.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(note => note.User)
            .WithMany(user => user.UserNotes)
            .HasForeignKey(note => note.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}