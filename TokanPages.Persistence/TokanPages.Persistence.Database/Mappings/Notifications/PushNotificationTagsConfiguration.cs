using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Notifications;

namespace TokanPages.Persistence.Database.Mappings.Notifications;

[ExcludeFromCodeCoverage]
public class PushNotificationTagsConfiguration : IEntityTypeConfiguration<PushNotificationTag>
{
    public void Configure(EntityTypeBuilder<PushNotificationTag> builder)
    {
        builder.Property(tags => tags.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(notificationTags => notificationTags.PushNotification)
            .WithMany(notifications => notifications.PushNotificationTags)
            .HasForeignKey(notificationTags => notificationTags.PushNotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PushNotificationTags_PushNotifications");
    }
}