using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Notifications;

namespace TokanPages.Persistence.Database.Mappings.Notifications;

[ExcludeFromCodeCoverage]
public class PushNotificationTagConfiguration : IEntityTypeConfiguration<PushNotificationTag>
{
    public void Configure(EntityTypeBuilder<PushNotificationTag> builder)
    {
        builder.Property(tag => tag.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(tag => tag.PushNotification)
            .WithMany(notification => notification.PushNotificationTags)
            .HasForeignKey(tag => tag.PushNotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}