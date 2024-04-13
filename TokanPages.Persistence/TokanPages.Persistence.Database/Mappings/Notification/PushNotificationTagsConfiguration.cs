using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Notification;

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